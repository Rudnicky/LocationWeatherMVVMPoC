using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LocationWeatherMVVMPoC
{
    public class ButtonExt : Grid
    {
        #region Properties & Fields

        public event EventHandler Clicked;

        private Grid coverLabel;
        public Grid CoverLabel
        {
            get { return coverLabel; }
            set { coverLabel = value; }
        }

        public static readonly BindableProperty ButtonColorProperty = BindableProperty.Create(
                nameof(ButtonColor),
                typeof(Color),
                typeof(ButtonExt),
                Color.Green,
                BindingMode.TwoWay,
                (bindable, value) => { return true; },
                (bindable, oldValue, newValue) => { ((ButtonExt)bindable).button.BackgroundColor = (Color)newValue; }
                );

        public static BindableProperty TextColorProperty = BindableProperty.Create(
                nameof(TextColor),
                typeof(Color),
                typeof(ButtonExt),
                Color.White,
                BindingMode.TwoWay,
                (bindable, value) => { return true; },
                (bindable, oldValue, newValue) =>
                {
                    ((ButtonExt)bindable).captionLabel.TextColor = (Color)newValue;
                }
                );

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
                nameof(BorderColor),
                typeof(Color),
                typeof(ButtonExt),
                Color.Transparent,
                BindingMode.TwoWay,
                (bindable, value) => { return true; },
                (bindable, oldValue, newValue) => { ((ButtonExt)bindable).button.BorderColor = (Color)newValue; }
                );

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(
                nameof(BorderWidth),
                typeof(double),
                typeof(ButtonExt),
                0d,
                BindingMode.TwoWay,
                (bindable, value) => { return true; },
                (bindable, oldValue, newValue) => { ((ButtonExt)bindable).button.BorderWidth = (double)newValue; }
                );

        public static readonly BindableProperty BorderRadiusProperty = BindableProperty.Create(
                nameof(BorderRadius),
                typeof(int),
                typeof(ButtonExt),
                0,
                BindingMode.TwoWay,
                (bindable, value) => { return true; },
                (bindable, oldValue, newValue) =>
                {
                    ((ButtonExt)bindable).button.BorderRadius = (int)newValue;
                }
                );

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
                nameof(FontSize),
                typeof(double),
                typeof(ButtonExt),
                20d,
                BindingMode.TwoWay,
                (bindable, value) => { return ((double)value > 0); },
                (bindable, oldValue, newValue) => { ((ButtonExt)bindable).captionLabel.FontSize = (double)newValue; }
                );

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(ButtonExt),
                "",
                BindingMode.TwoWay,
                (bindable, value) => { return true; },
                (bindable, oldValue, newValue) =>
                {
                    ((ButtonExt)bindable).captionLabel.Text = (string)newValue;
                }
                );

        new public static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(
            nameof(IsEnabled),
            typeof(bool),
            typeof(ButtonExt),
            true,
                BindingMode.TwoWay,
                (bindable, value) => { return true; },
                (bindable, oldValue, newValue) =>
                {
                    bool isEnabled = (bool)newValue;
                    var buttonExt = ((ButtonExt)bindable);

                    buttonExt.button.IsEnabled = isEnabled;
                    buttonExt.IsEnabled = isEnabled;
                }
                );

        public Color ButtonColor
        {
            get { return (Color)GetValue(ButtonColorProperty); }
            set { SetValue(ButtonColorProperty, value); }
        }

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public double BorderWidth
        {
            get { return (double)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        public int BorderRadius
        {
            get { return (int)GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }

        new public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        private CustomBtn button = new CustomBtn()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            BorderColor = Color.Transparent
        };

        private Label captionLabel = new Label()
        {
            HorizontalTextAlignment = TextAlignment.Center,
            Margin = 2,
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        private TapGestureRecognizer tapGestureRecognizer;

        #endregion

        #region Ctor
        public ButtonExt()
        {
            CoverLabel = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            this.RowSpacing = this.ColumnSpacing = 0;
            tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnTapped;

            CoverLabel.GestureRecognizers.Add(tapGestureRecognizer);

            this.Children.Add(button);
            this.Children.Add(captionLabel);
            this.Children.Add(CoverLabel);
        }

        private async void OnTapped(object sender, EventArgs args)
        {
            if (this.IsEnabled)
            {
                IsEnabled = false;
                Clicked?.Invoke(this, args);

                await Task.Delay(500);
                IsEnabled = true;
            }
        }
        #endregion
    }
}
