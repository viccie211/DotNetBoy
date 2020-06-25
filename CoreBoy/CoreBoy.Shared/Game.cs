using System;
using System.Collections.Generic;
using Ultraviolet;
using Ultraviolet.BASS;
using Ultraviolet.FreeType2;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;
using Ultraviolet.Input;
using Ultraviolet.OpenGL;
using CoreBoy.Models;
using System.Linq;
using CoreBoy.Emulator;
using System.IO;

namespace CoreBoy
{
    public partial class Game : UltravioletApplication
    {
        private const int _scale = 4;
        private const int _width = 160;
        private const int _height = 144;
        private readonly Color _black = new Color(0f, 0f, 0f);
        private readonly Color _dark = new Color(0.33f, 0.33f, 0.33f);
        private readonly Color _light = new Color(0.66f, 0.66f, 0.66f);
        private readonly Color _white = new Color(1f, 1f, 1f);
        private int xOffSet = 0;
        private int yOffSet = 0;
        int direction = 0;
        private int stepCounter = 0;
        private const int stepThreshold = 10;
        private Texture2D _frameBuffer;
        private Surface2D _canvas;
        private List<Color> _colorList;

        private int _tileMapNr = 0;
        private int _tileSetNr = 0;
        private CPU cpu;
        private Colors[,] _frameData = new Colors[_height, _width];
        private TileData tileData = new TileData();

        public byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public Game()
            : base("viccie211", "CoreBoy")
        {
            cpu = new CPU();
            cpu._MMU.LoadRom(File.ReadAllBytes("C:\\Users\\VictorRemmerswaal\\Downloads\\bgbw64\\bgbtest.gb"));
            cpu.Loop();
        }

        protected override UltravioletContext OnCreatingUltravioletContext()
        {
            var configuration = new OpenGLUltravioletConfiguration();
            configuration.Plugins.Add(new BASSAudioPlugin());
            configuration.Plugins.Add(new FreeTypeFontPlugin());

#if DEBUG
            configuration.Debug = true;
            configuration.DebugLevels = DebugLevels.Error | DebugLevels.Warning;
            configuration.DebugCallback = (uv, level, message) =>
            {
                System.Diagnostics.Debug.WriteLine(message);
            };
#endif

            return new OpenGLUltravioletContext(this, configuration);
        }

        protected override void OnInitialized()
        {
            UsePlatformSpecificFileSource();
            _frameBuffer = Texture2D.CreateTexture(_width * _scale, _height * _scale);
            _canvas = Surface2D.Create(_width * _scale, _height * _scale);
            this.spriteBatch = SpriteBatch.Create();
            base.OnInitialized();
        }

        protected override void OnLoadingContent()
        {
            base.OnLoadingContent();
        }

        protected override void OnUpdating(UltravioletTime time)
        {
            if (Ultraviolet.GetInput().GetKeyboard().IsKeyPressed(Key.Escape))
            {
                Exit();
            }
            base.OnUpdating(time);
        }

        protected override void OnDrawing(UltravioletTime time)
        {
            var window = Ultraviolet.GetPlatform().Windows.GetCurrent();
            var input = Ultraviolet.GetInput();
            if (input.GetKeyboard().IsKeyDown(Key.Backslash))
            {
                window.Position = new Point2(0, 40);
            }
            this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            _colorList = new List<Color>();
            SetFrameData();
            for (int y = 0; y < _height; y++)
            {
                for (int yScale = 0; yScale < _scale; yScale++)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        for (int xScale = 0; xScale < _scale; xScale++)
                        {
                            switch (_frameData[y, x])
                            {
                                case (Colors)0:
                                    _colorList.Add(_white);
                                    break;
                                case (Colors)1:
                                    _colorList.Add(_dark);
                                    break;
                                case (Colors)2:
                                    _colorList.Add(_light);
                                    break;
                                case (Colors)3:
                                    _colorList.Add(_black);
                                    break;
                            }
                        }
                    }
                }
            }
            _canvas.Clear(_black);
            _canvas.SetData(_colorList.ToArray());
            _canvas.Flip(SurfaceFlipDirection.Vertical);
            _frameBuffer.SetData(_canvas);
            this.spriteBatch.Flush();
            this.spriteBatch.Draw(_frameBuffer, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 1, SpriteEffects.None, 0f);
            this.spriteBatch.End();

            _canvas.Clear(_black);
            if (stepCounter == stepThreshold)
            {


                switch (direction)
                {
                    case 0:
                        xOffSet -= 1;
                        if (xOffSet <= -(_width / 2))
                        {
                            direction = 1;
                        }
                        break;
                    case 1:
                        yOffSet -= 1;
                        if (yOffSet <= -(_height / 2))
                        {
                            direction = 2;
                        }
                        break;
                    case 2:
                        xOffSet += 1;
                        if (xOffSet >= 0)
                        {
                            direction = 3;
                        }
                        break;
                    case 3:
                        yOffSet += 1;
                        if (yOffSet >= 0)
                        {
                            direction = 0;
                        }
                        break;
                }
                stepCounter = 0;
            }
            else
            {
                stepCounter++;
            }
            base.OnDrawing(time);
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                if (this.spriteBatch != null)
                    this.spriteBatch.Dispose();
            }
            base.Dispose(disposing);
        }

        private SpriteBatch spriteBatch;

        private void SetFrameData()
        {
            for (int y = 0; y < 0x20; y++)
            {
                for (int x = 0; x < 0x20; x++)
                {

                    Tile tile = tileData.TileSet.GetByTileAndSetNr(tileData.TileMap.GetTileNrFromTileMap(y, x, _tileMapNr), _tileSetNr);

                    for (int tiley = 0; tiley < 8; tiley++)
                    {
                        for (int tilex = 0; tilex < 8; tilex++)
                        {
                            int screenX = x * 8 + tilex + xOffSet;
                            int screenY = y * 8 + tiley + yOffSet;
                            if (screenY >= 0 && screenY < _height && screenX >= 0 && screenX < _width)
                            {
                                _frameData[screenY, screenX] = tile.Pixels[tiley, tilex];
                            }
                        }
                    }
                }
            }
        }
    }
}
