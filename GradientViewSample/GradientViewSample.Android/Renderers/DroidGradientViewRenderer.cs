using Android.Content;
using Android.Graphics.Drawables;
using GradientViewSample.Controls;
using GradientViewSample.Droid.Renderers;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;

[assembly: Xamarin.Forms.ExportRenderer(typeof(GradientView), typeof(DroidGradientViewRenderer))]
namespace GradientViewSample.Droid.Renderers
{
    public class DroidGradientViewRenderer : VisualElementRenderer<ContentView>
    {
        public DroidGradientViewRenderer(Context context)
            : base(context)
        {
        }

        private Color StartColor { get; set; }
        private Color EndColor { get; set; }
        private StackOrientation Orientation { get; set; }

        protected override void OnElementChanged(ElementChangedEventArgs<ContentView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {
                var stack = (GradientView)e.NewElement;
                StartColor = stack.StartColor;
                EndColor = stack.EndColor;
                Orientation = stack.Orientation;

                SetGradientBackground();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR:", ex.Message);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == nameof(GradientView.StartColor) || e.PropertyName == nameof(GradientView.EndColor))
            {
                var stack = (GradientView)sender;
                if (stack == null)
                {
                    return;
                }

                StartColor = stack.StartColor;
                EndColor = stack.EndColor;
                Orientation = stack.Orientation;

                SetGradientBackground();
            }
        }

        private void SetGradientBackground()
        {
            int[] colors = { StartColor.ToAndroid(), EndColor.ToAndroid() };
            GradientDrawable gradient = new GradientDrawable(
                Orientation == StackOrientation.Horizontal
                ? GradientDrawable.Orientation.LeftRight
                : GradientDrawable.Orientation.TopBottom,
                colors);
            Android.Support.V4.View.ViewCompat.SetBackground(this, gradient);
        }
    }
}