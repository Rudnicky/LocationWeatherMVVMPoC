using System;
using Xamarin.Forms;

namespace LocationWeatherMVVMPoC
{
    public class BorderedEntry : ContentView
    {
        #region Properties & Fields

        public event EventHandler<TextChangedEventArgs> TextChanged;
        public event EventHandler<EventArgs> Completed;
        public new event EventHandler<FocusEventArgs> Focused;
        public new event EventHandler<FocusEventArgs> Unfocused;

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(BorderedEntry), Color.Black);

        public static readonly BindableProperty FocusedBorderColorProperty =
            BindableProperty.Create(nameof(FocusedBorderColor), typeof(Color), typeof(BorderedEntry), Color.Blue);

        public static readonly BindableProperty DisabledBorderColorProperty =
            BindableProperty.Create(nameof(DisabledBorderColor), typeof(Color), typeof(BorderedEntry), Color.Gray);

        public static readonly BindableProperty InvalidBorderColorProperty =
            BindableProperty.Create(nameof(InvalidBorderColor), typeof(Color), typeof(BorderedEntry), Color.Red);

        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(nameof(MaxTextLength), typeof(int?), typeof(BorderedEntry), null);

        public static readonly BindableProperty MaxNumericValueProperty =
            BindableProperty.Create(nameof(MaxNumericValue), typeof(int?), typeof(BorderedEntry), null);

        new public static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(
                nameof(IsEnabled),
                typeof(bool),
                typeof(BorderedEntry),
                true,
                BindingMode.TwoWay,
                (bindable, value) => { return true; }, // Validator
                (bindable, oldValue, newValue) =>
                {
                    ((BorderedEntry)bindable).Entry.IsEnabled = (bool)newValue;
                    ((BorderedEntry)bindable).IsEnabled = (bool)newValue;
                }
                );

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
                nameof(IsPassword),
                typeof(bool),
                typeof(BorderedEntry),
                false,
                BindingMode.TwoWay,
                (bindable, value) => { return true; }, // Validator
                (bindable, oldValue, newValue) => { ((BorderedEntry)bindable).Entry.IsPassword = (bool)newValue; }
                );

        public static readonly BindableProperty TextAlignmentProperty = BindableProperty.Create(
                nameof(TextAlignment),
                typeof(TextAlignment),
                typeof(BorderedEntry),
                TextAlignment.Start,
                BindingMode.TwoWay,
                (bindable, value) => { return true; }, // Validator
                (bindable, oldValue, newValue) => { ((BorderedEntry)bindable).Entry.HorizontalTextAlignment = (TextAlignment)newValue; }
                );

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(BorderedEntry),
                "",
                BindingMode.TwoWay,
                (bindable, value) => { return true; }, // Validator
                (bindable, oldValue, newValue) =>
                {
                    ((BorderedEntry)bindable).Entry.Text = (string)newValue;
                }
                );

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
                typeof(string),
                typeof(BorderedEntry),
                "",
                BindingMode.TwoWay,
                (bindable, value) => { return true; }, // Validator
                (bindable, oldValue, newValue) =>
                {
                    ((BorderedEntry)bindable).Entry.Placeholder = (string)newValue;
                }
                );

        public static readonly BindableProperty ValueInvalidProperty = BindableProperty.Create(
                nameof(ValueInvalid),
                typeof(bool),
                typeof(BorderedEntry),
                false,
                BindingMode.TwoWay,
                (bindable, value) => { return true; }, // Validator
                (bindable, oldValue, newValue) =>
                {
                    var borderedEntry = (BorderedEntry)bindable;
                    var entry = borderedEntry.Entry;
                    var valueInvalid = (bool)newValue;

                    if (valueInvalid)
                    {
                        borderedEntry.BackgroundColor = borderedEntry.InvalidBorderColor;
                    }
                    else
                    {
                        borderedEntry.BackgroundColor = entry.IsFocused ?
                            borderedEntry.FocusedBorderColor : borderedEntry.BackgroundColor;
                    }
                }
                );

        public bool ValueInvalid
        {
            get { return (bool)GetValue(ValueInvalidProperty); }
            set { SetValue(ValueInvalidProperty, value); }
        }

        new public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public Color FocusedBorderColor
        {
            get { return (Color)GetValue(FocusedBorderColorProperty); }
            set { SetValue(FocusedBorderColorProperty, value); }
        }

        public Color DisabledBorderColor
        {
            get { return (Color)GetValue(DisabledBorderColorProperty); }
            set { SetValue(DisabledBorderColorProperty, value); }
        }

        public Color InvalidBorderColor
        {
            get { return (Color)GetValue(InvalidBorderColorProperty); }
            set { SetValue(InvalidBorderColorProperty, value); }
        }

        public string Placeholder
        {
            get { return GetValue(PlaceholderProperty) as string; }
            set { SetValue(PlaceholderProperty, value); }
        }

        public int? MaxTextLength
        {
            get { return (int?)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public int? MaxNumericValue
        {
            get { return (int?)GetValue(MaxNumericValueProperty); }
            set { SetValue(MaxNumericValueProperty, value); }
        }

        private Keyboard keyboard;
        public Keyboard Keyboard
        {
            get { return keyboard; }
            set { keyboard = value; Entry.Keyboard = keyboard; }
        }

        public Entry Entry { get; private set; }

        public int Tag { get; set; }

        private bool initialized;
        private bool constructorCodeExecutionFinished;
        #endregion Properties

        #region Ctor
        public BorderedEntry()
        {
            this.Entry = new Entry()
            {
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            this.Entry.Focused += (o, e) =>
            {
                this.BackgroundColor = ValueInvalid ? this.InvalidBorderColor : this.FocusedBorderColor;
                HandleFocused(o, e);
            };

            this.Entry.Unfocused += (o, e) =>
            {
                this.BackgroundColor = ValueInvalid ? this.InvalidBorderColor : this.BorderColor;
                Unfocused?.Invoke(o, e);
            };

            this.Entry.TextChanged += HandleTextChanged;

            this.Entry.Completed += HandleCompleted;

            this.Padding = new Thickness(1);
            this.Content = this.Entry;

            constructorCodeExecutionFinished = true;
        }
        #endregion

        #region Methods

        protected override void OnSizeAllocated(double width, double height)
        {
            // WORKAROUND:
            // At the moment Xamarin doesn't offer better place to do control after-loading initializatiom.

            // This method is called twice: before and after constructor. We want to set everything in the
            // second pass. Because this method could be called also on orientation change, _initialized
            // variable was added.

            if (!initialized && constructorCodeExecutionFinished)
            {
                this.BackgroundColor = this.BorderColor;
                this.Entry.Placeholder = this.Placeholder;
                initialized = true;
            }

            base.OnSizeAllocated(width, height);
        }

        public void ClearText()
        {
            Text = string.Empty;
        }

        private void HandleCompleted(object sender, EventArgs e)
        {
            Completed?.Invoke(this, e);
        }

        private void HandleFocused(object sender, FocusEventArgs e)
        {
            Focused?.Invoke(this, e);
        }

        private void HandleTextChanged(object sender, TextChangedEventArgs args)
        {
            decimal numericValue;
            var entryText = args.NewTextValue;

            if (MaxTextLength.HasValue
                && decimal.TryParse(entryText, out numericValue)
                && Math.Truncate(numericValue).ToString().Length > MaxTextLength.Value)
            {
                entryText = entryText.Remove(entryText.Length - 1);
            }

            if (MaxNumericValue.HasValue
                && decimal.TryParse(entryText, out numericValue)
                && numericValue > MaxNumericValue.Value)
            {
                entryText = entryText.Remove(entryText.Length - 1);
            }

            Entry.Text = entryText;
            Text = entryText;

            TextChanged?.Invoke(this, args);
        }

        new public bool Focus()
        {
            return Entry.Focus();
        }
        #endregion
    }
}

