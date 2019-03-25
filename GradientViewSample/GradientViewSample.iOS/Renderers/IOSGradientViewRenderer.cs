using System.ComponentModel;
using CoreAnimation;
using CoreGraphics;
using GradientViewSample.iOS.Renderers;
using GradientViewSample.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GradientView), typeof(IOSGradientViewRenderer))]
namespace GradientViewSample.iOS.Renderers
{
    public class IOSGradientViewRenderer : VisualElementRenderer<ContentView>
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            GradientView layout = (GradientView)Element;

            CGColor startColor = layout.StartColor.ToCGColor();
            CGColor endColor = layout.EndColor.ToCGColor();
            StackOrientation orientation = layout.Orientation;

            CAGradientLayer gradientLayer = new CAGradientLayer();
            if (orientation == StackOrientation.Horizontal)
            {
                gradientLayer = new CAGradientLayer()
                {
                    StartPoint = new CGPoint(0, 0.5),
                    EndPoint = new CGPoint(1, 0.5)
                };
            }
            else
            {
                gradientLayer = new CAGradientLayer()
                {
                    StartPoint = new CGPoint(0.5, 1),
                    EndPoint = new CGPoint(0.5, 0)
                };
            }

            gradientLayer.Frame = rect;
            gradientLayer.Colors = new CGColor[]
            {
                startColor,
                endColor
            };

            NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            SetNeedsDisplay();
        }
    }
}