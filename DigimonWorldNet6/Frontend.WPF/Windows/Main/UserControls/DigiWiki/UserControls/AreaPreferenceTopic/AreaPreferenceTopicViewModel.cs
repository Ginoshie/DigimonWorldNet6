using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.AreaPreferenceTopic;

public class AreaPreferenceTopicViewModel : TopicViewModelBase
{
    private const string NEGATIVE_DISLIKED_IMAGE_1_PATH = "Media/happiness-bar-negative-disliked-1.jpg";
    private const string NEGATIVE_DISLIKED_IMAGE_2_PATH = "Media/happiness-bar-negative-disliked-2.jpg";
    private const string NEGATIVE_LIKED_IMAGE_1_PATH = "Media/happiness-bar-negative-liked-1.jpg";
    private const string NEGATIVE_LIKED_IMAGE_2_PATH = "Media/happiness-bar-negative-liked-2.jpg";
    private const string POSITIVE_DISLIKED_IMAGE_1_PATH = "Media/happiness-bar-positive-disliked-1.jpg";
    private const string POSITIVE_DISLIKED_IMAGE_2_PATH = "Media/happiness-bar-positive-disliked-2.jpg";
    private const string POSITIVE_LIKED_IMAGE_1_PATH = "Media/happiness-bar-positive-liked-1.jpg";
    private const string POSITIVE_LIKED_IMAGE_2_PATH = "Media/happiness-bar-positive-liked-2.jpg";
    
    private DispatcherTimer _timer;
    private bool _showImage1;
    private bool _isRunning;
    private ImageSource _negativeDislikedImageSource = new BitmapImage(new Uri(NEGATIVE_DISLIKED_IMAGE_1_PATH, UriKind.Relative));
    private ImageSource _negativeLikedImageSource = new BitmapImage(new Uri(NEGATIVE_LIKED_IMAGE_1_PATH, UriKind.Relative));
    private ImageSource _positiveDislikedDislikedImageSource = new BitmapImage(new Uri(POSITIVE_DISLIKED_IMAGE_1_PATH, UriKind.Relative));
    private ImageSource _positiveLikedImageSource = new BitmapImage(new Uri(POSITIVE_LIKED_IMAGE_1_PATH, UriKind.Relative));

    public AreaPreferenceTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.AreaPreferenceWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.AreaPreferenceWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.AreaPreferenceWiki.WikiText, SpeakShellmonTextAction);

        SpeakAreaPreferenceHappinessBarCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.AreaPreferenceWiki.HappinessBar, SpeakShellmonTextAction));
        OpenGuideAreaPreferenceChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideAreaPreferenceChapter, UseShellExecute = true }));

        SetupAnimationTimer();

        ToggleAnimationCommand = new CommandHandler(ToggleAnimation);
    }

    public ICommand ToggleAnimationCommand { get; }
    public ICommand SpeakAreaPreferenceHappinessBarCommand { get; }
    public ICommand OpenGuideAreaPreferenceChapterCommand { get; }

    public bool IsRunning
    {
        get => _isRunning;
        private set
        {
            _isRunning = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(AnimationButtonIcon));
            OnPropertyChanged(nameof(AnimationToolTipText));
        }
    }
    
    public ImageSource NegativeDislikedImageSource
    {
        get => _negativeDislikedImageSource;
        set
        {
            _negativeDislikedImageSource = value;
            OnPropertyChanged();
        }
    }
    
    public ImageSource NegativeLikedImageSource
    {
        get => _negativeLikedImageSource;
        set
        {
            _negativeLikedImageSource = value;
            OnPropertyChanged();
        }
    }
    
    public ImageSource PositiveDislikedImageSource
    {
        get => _positiveDislikedDislikedImageSource;
        set
        {
            _positiveDislikedDislikedImageSource = value;
            OnPropertyChanged();
        }
    }
    
    public ImageSource PositiveLikedImageSource
    {
        get => _positiveLikedImageSource;
        set
        {
            _positiveLikedImageSource = value;
            OnPropertyChanged();
        }
    }
    
    public string AnimationButtonIcon =>
        IsRunning ? "||" : "▶️";

    public string AnimationToolTipText =>
        IsRunning ? "Stop animation" : "Start animation";

    private void SetupAnimationTimer()
    {
        IsRunning = false;
        _showImage1  = false;
        
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(500)
        };
        _timer.Tick += (s, e) =>
        {
            SwapImages();
        };
    }

    private void ToggleAnimation()
    {
        if (_isRunning)
        {
            _timer.Stop();
        }
        else
        {
            SwapImages();
            _timer.Start();
        }

        IsRunning = !IsRunning;
    }

    private void SwapImages()
    {
        PositiveDislikedImageSource = new BitmapImage(
            new Uri(_showImage1
                    ? POSITIVE_DISLIKED_IMAGE_1_PATH
                    : POSITIVE_DISLIKED_IMAGE_2_PATH,
                UriKind.Relative));
        
        NegativeDislikedImageSource = new BitmapImage(
            new Uri(_showImage1
                    ? NEGATIVE_DISLIKED_IMAGE_1_PATH
                    : NEGATIVE_DISLIKED_IMAGE_2_PATH,
                UriKind.Relative));
        PositiveLikedImageSource = new BitmapImage(
            new Uri(_showImage1
                    ? POSITIVE_LIKED_IMAGE_1_PATH
                    : POSITIVE_LIKED_IMAGE_2_PATH,
                UriKind.Relative));
        NegativeLikedImageSource = new BitmapImage(
            new Uri(_showImage1
                    ? NEGATIVE_LIKED_IMAGE_1_PATH
                    : NEGATIVE_LIKED_IMAGE_2_PATH,
                UriKind.Relative));
            
        _showImage1 = !_showImage1;
    }
}