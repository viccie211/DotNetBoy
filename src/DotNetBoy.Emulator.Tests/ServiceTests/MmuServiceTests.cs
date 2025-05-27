using DotNetBoy.Emulator.Services.Implementations;

namespace DotNetBoy.Emulator.Tests.ServiceTests;

public class MmuServiceTests
{
    private MmuService mmuService;

    [SetUp]
    public void SetUp()
    {
        var byteUshortServiceMock = new Mock<IByteUshortService>();
        var timerServiceMock = new Mock<ITimerService>();
        var eventServiceMock = new Mock<IEventService>();
        var joypadServiceMock = new Mock<JoyPadService>();
        byteUshortServiceMock.Setup(b => b.CombineBytes(0x00, 0xFF)).Returns(0x00FF);

        mmuService = new MmuService(byteUshortServiceMock.Object, timerServiceMock.Object, eventServiceMock.Object, joypadServiceMock.Object)
        {
            MappedMemory =
            {
                [0x0000] = 0xFF,
                [0x0001] = 0x00,
                [0x7FFF] = 0xFF,
                [0x8000] = 0xFF,
                [0xC000] = 0xFF,
                [0xDDFF] = 0xFF,
                [0xE000] = 0xFF,
                [0xFDFF] = 0xFF,
            }
        };
    }

    [Test]
    public void ReadByte0x0000()
    {
        byte expected = 0xFF;
        var result = mmuService.ReadByte(0x0000);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ReadWordLittleEndian0x0000()
    {
        ushort expected = 0x00FF;
        var result = mmuService.ReadWordLittleEndian(0x0000);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void CantWriteByteTo0x0000()
    {
        ushort address = 0x0000;
        byte toWrite = 0x00;
        byte expected = 0xFF;
        mmuService.WriteByte(address, toWrite);
        var result = mmuService.MappedMemory[address];
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(result, Is.Not.EqualTo(toWrite));
    }

    [Test]
    public void CantWriteByteTo0x7FFF()
    {
        ushort address = 0x7FFF;
        byte toWrite = 0x00;
        byte expected = 0xFF;
        mmuService.WriteByte(address, toWrite);
        var result = mmuService.MappedMemory[address];
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(result, Is.Not.EqualTo(toWrite));
    }

    [Test]
    public void CanWriteByteTo0x8000()
    {
        ushort address = 0x8000;
        byte toWrite = 0x00;
        byte expected = 0x00;
        var original = mmuService.MappedMemory[address];
        mmuService.WriteByte(address, toWrite);
        var result = mmuService.MappedMemory[address];
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(result, Is.EqualTo(toWrite));
        Assert.That(result, Is.Not.EqualTo(original));
    }

    [Test]
    public void WritesBetween0xC000And0xDDFFAreDuplicated()
    {
        ushort address = 0xC000;
        ushort addressDuplicate = 0xE000;
        byte toWrite = 0x00;
        byte expected = 0x00;
        var original = mmuService.MappedMemory[address];
        var originalDuplicate = mmuService.MappedMemory[addressDuplicate];
        mmuService.WriteByte(address, toWrite);
        var result = mmuService.MappedMemory[address];
        var resultDuplicate = mmuService.MappedMemory[addressDuplicate];
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(result, Is.EqualTo(toWrite));
        Assert.That(resultDuplicate, Is.EqualTo(expected));
        Assert.That(resultDuplicate, Is.EqualTo(toWrite));
        Assert.That(result, Is.Not.EqualTo(original));
        Assert.That(result, Is.Not.EqualTo(original));
        Assert.That(resultDuplicate, Is.Not.EqualTo(originalDuplicate));
        Assert.That(resultDuplicate, Is.Not.EqualTo(originalDuplicate));

        address = 0xDDFF;
        addressDuplicate = 0xFDFF;
        toWrite = 0x00;
        expected = 0x00;
        original = mmuService.MappedMemory[address];
        originalDuplicate = mmuService.MappedMemory[addressDuplicate];
        mmuService.WriteByte(address, toWrite);
        result = mmuService.MappedMemory[address];
        resultDuplicate = mmuService.MappedMemory[addressDuplicate];
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(result, Is.EqualTo(toWrite));
        Assert.That(resultDuplicate, Is.EqualTo(expected));
        Assert.That(resultDuplicate, Is.EqualTo(toWrite));
        Assert.That(result, Is.Not.EqualTo(original));
        Assert.That(result, Is.Not.EqualTo(original));
        Assert.That(resultDuplicate, Is.Not.EqualTo(originalDuplicate));
        Assert.That(resultDuplicate, Is.Not.EqualTo(originalDuplicate));
    }

    [Test]
    public void WritesBetween0xE000And0xFDFFAreDuplicated()
    {
        ushort address = 0xE000;
        ushort addressDuplicate = 0xC000;
        byte toWrite = 0x00;
        byte expected = 0x00;
        var original = mmuService.MappedMemory[address];
        var originalDuplicate = mmuService.MappedMemory[addressDuplicate];
        mmuService.WriteByte(address, toWrite);
        var result = mmuService.MappedMemory[address];
        var resultDuplicate = mmuService.MappedMemory[addressDuplicate];
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(result, Is.EqualTo(toWrite));
        Assert.That(resultDuplicate, Is.EqualTo(expected));
        Assert.That(resultDuplicate, Is.EqualTo(toWrite));
        Assert.That(result, Is.Not.EqualTo(original));
        Assert.That(result, Is.Not.EqualTo(original));
        Assert.That(resultDuplicate, Is.Not.EqualTo(originalDuplicate));
        Assert.That(resultDuplicate, Is.Not.EqualTo(originalDuplicate));

        address = 0xFDFF;
        addressDuplicate = 0xDDFF;
        toWrite = 0x00;
        expected = 0x00;
        original = mmuService.MappedMemory[address];
        originalDuplicate = mmuService.MappedMemory[addressDuplicate];
        mmuService.WriteByte(address, toWrite);
        result = mmuService.MappedMemory[address];
        resultDuplicate = mmuService.MappedMemory[addressDuplicate];
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(result, Is.EqualTo(toWrite));
        Assert.That(resultDuplicate, Is.EqualTo(expected));
        Assert.That(resultDuplicate, Is.EqualTo(toWrite));
        Assert.That(result, Is.Not.EqualTo(original));
        Assert.That(result, Is.Not.EqualTo(original));
        Assert.That(resultDuplicate, Is.Not.EqualTo(originalDuplicate));
        Assert.That(resultDuplicate, Is.Not.EqualTo(originalDuplicate));
    }

    [Test]
    public void LoadRom()
    {
        ushort romLength = 0x8000;
        var rom = new byte[romLength];

        for (int i = 0; i < rom.Length; i++)
        {
            rom[i] = (byte)(i % 256);
        }

        mmuService.LoadRom(rom);
        var result = mmuService.MappedMemory;

        for (int i = 0; i < romLength; i++)
        {
            Assert.That(result[i], Is.EqualTo(rom[i]));
        }
    }

    [Test]
    public void LoadRomDoesntLoad0x8000AndBeyond()
    {
        ushort romLength = 0x8001;
        var rom = new byte[romLength];

        for (int i = 0; i < rom.Length; i++)
        {
            rom[i] = (byte)(i % 256);
        }

        byte notExpected = 31;
        rom[0x8000] = notExpected;

        mmuService.LoadRom(rom);
        var result = mmuService.MappedMemory;
        for (int i = 0; i < romLength; i++)
        {
            if (i != 0x8000)
            {
                Assert.That(result[i], Is.EqualTo(rom[i]));
            }
            else
            {
                Assert.That(result[i], Is.Not.EqualTo(notExpected));
            }
        }
    }
}