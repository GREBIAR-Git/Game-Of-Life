using GameOfLife.Commands;
using GameOfLife.Model;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WordKiller.ViewModels;

namespace GameOfLife.ViewModels;

class ViewModelMain : ViewModelBase
{
    readonly MapOneColor map = new();

    bool pause;
    public bool Pause { get => pause; set => SetProperty(ref pause, value); }

    int delay;
    public int Delay { get => delay; set => SetProperty(ref delay, value); }

    Neighbours newNeighbours;
    public Neighbours NewNeighbours { get => newNeighbours; set => SetProperty(ref newNeighbours, value); }

    Neighbours oldNeighbours;

    public Neighbours OldNeighbours { get => oldNeighbours; set => SetProperty(ref oldNeighbours, value); }

    ICommand? randomDistribution;

    public ICommand RandomDistribution
    {
        get
        {
            return randomDistribution ??= new RelayCommand(
            obj =>
            {
                map.SetRandom();
            });
        }
    }

    ICommand? clear;

    public ICommand Clear
    {
        get
        {
            return clear ??= new RelayCommand(
            obj =>
            {
                map.Clear();
            });
        }
    }

    ICommand? nextStep;

    public ICommand NextStep
    {
        get
        {
            return nextStep ??= new RelayCommand(
            obj =>
            {
                map.NextState(newNeighbours, oldNeighbours);
            });
        }
    }


    [NonSerialized]
    ImageSource? bitmapImage;

    public ImageSource? BitmapImage
    {
        get => bitmapImage;
        set
        {
            SetProperty(ref bitmapImage, value);
        }
    }


    void ImageLooping()
    {
        while (true)
        {
            Application.Current.Dispatcher.Invoke(() => BitmapImage = ImageGeneration());
            if (!Pause)
            {
                map.NextState(newNeighbours, oldNeighbours);
            }
            Thread.Sleep(Delay);
        }
    }

    WriteableBitmap ImageGeneration()
    {
        WriteableBitmap wb = new(Map.Size,
            Map.Size, 96, 96, PixelFormats.Bgra32, null);

        Int32Rect rect = new(0, 0, Map.Size, Map.Size);

        byte[] pixels = new byte[Map.Size * Map.Size * wb.Format.BitsPerPixel / 8];
        for (int y = 0; y < wb.PixelHeight; y++)
        {
            for (int x = 0; x < wb.PixelWidth; x++)
            {
                int pos = y * Map.Size + x;
                if (map[pos] == 1)
                {
                    int pixelOffset = (x + y * wb.PixelWidth) * wb.Format.BitsPerPixel / 8;
                    pixels[pixelOffset] = 78;
                    pixels[pixelOffset + 1] = 78;
                    pixels[pixelOffset + 2] = 80;
                    pixels[pixelOffset + 3] = 255;
                }
            }

            int stride = wb.PixelWidth * wb.Format.BitsPerPixel / 8;

            wb.WritePixels(rect, pixels, stride, 0);
        }
        return wb;
    }


    public ViewModelMain()
    {
        map.Block();
        Delay = 250;
        newNeighbours = new();
        oldNeighbours = new();
        newNeighbours.Three = true;
        oldNeighbours.Two = true;
        oldNeighbours.Three = true;
        Thread myThread = new(ImageLooping);
        myThread.Start();
    }
}
