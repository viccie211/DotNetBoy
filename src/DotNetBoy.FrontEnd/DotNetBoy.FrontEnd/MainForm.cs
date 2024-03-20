using System;
using System.IO;
using System.Threading.Tasks;
using DotNetBoy.Emulator;
using DotNetBoy.Emulator.Extensions;
using DotNetBoy.Emulator.Services.Interfaces;
using Eto.Forms;
using Eto.Drawing;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBoy.FrontEnd
{
    public partial class MainForm : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public MainForm(IServiceCollection serviceCollection)
        {
            serviceCollection.AddEmulator();
            _serviceProvider = serviceCollection.BuildServiceProvider();
            var scope = _serviceProvider.CreateScope();
            var cpuRegisters = scope.ServiceProvider.GetService<ICpuRegistersService>()!;
            cpuRegisters.Reset();

            var rom = File.ReadAllBytes("..\\..\\..\\..\\..\\DotNetBoy\\DebugAssets\\bgbw64\\06-ld r,r.gb");
            var mmuService = scope.ServiceProvider.GetService<IMmuService>()!;
            mmuService.LoadRom(rom);
            var cpu = scope.ServiceProvider.GetService<Cpu>();
            if (cpu != null)
            {
                var task = new Task(() => cpu.Loop());
                task.Start();
            }

            var screen = new EmulatorScreen();

            Title = "DotNetBoy";
            MinimumSize = new Size(200, 200);

            Content = new StackLayout
            {
                Padding = 10,
                Items =
                {
                    screen.Bitmap
                }
            };

            // create a few commands that can be used for the menu and toolbar
            var clickMe = new Command { MenuText = "Click Me!", ToolBarText = "Click Me!" };
            clickMe.Executed += (sender, e) => MessageBox.Show(this, "I was clicked!");

            var quitCommand = new Command
                { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            quitCommand.Executed += (sender, e) => Application.Instance.Quit();

            var aboutCommand = new Command { MenuText = "About..." };
            aboutCommand.Executed += (sender, e) => new AboutDialog().ShowDialog(this);

            // create menu
            Menu = new MenuBar
            {
                Items =
                {
                    // File submenu
                    new SubMenuItem { Text = "&File", Items = { clickMe } },
                    // new SubMenuItem { Text = "&Edit", Items = { /* commands/items */ } },
                    // new SubMenuItem { Text = "&View", Items = { /* commands/items */ } },
                },
                ApplicationItems =
                {
                    // application (OS X) or file menu (others)
                    new ButtonMenuItem { Text = "&Preferences..." },
                },
                QuitItem = quitCommand,
                AboutItem = aboutCommand
            };

            // create toolbar			
            ToolBar = new ToolBar { Items = { clickMe } };
        }
    }
}