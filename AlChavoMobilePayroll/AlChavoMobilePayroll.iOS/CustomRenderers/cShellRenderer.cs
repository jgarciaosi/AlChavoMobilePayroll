using AlChavoMobilePayroll.iOS.CustomRenderers;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Shell), typeof(cShellRenderer))]
namespace AlChavoMobilePayroll.iOS.CustomRenderers
{
    public class cShellRenderer : ShellRenderer
    {
        protected override IShellPageRendererTracker CreatePageRendererTracker()
        {
            return new CustomShellPageRendererTracker(this);
        }
    }
    public class CustomShellPageRendererTracker : ShellPageRendererTracker
    {
        public CustomShellPageRendererTracker(IShellContext context)
            : base(context)
        {
        }

        protected override void UpdateTitleView()
        {
            if (ViewController == null || ViewController.NavigationItem == null)
                return;

            var titleView = Shell.GetTitleView(Page);

            if (titleView == null)
            {
                var view = ViewController.NavigationItem.TitleView;
                ViewController.NavigationItem.TitleView = null;
                view?.Dispose();
            }
            else
            {
                var view = new CustomTitleViewContainer(titleView);
                ViewController.NavigationItem.TitleView = view;
            }
        }
    }

    public class CustomTitleViewContainer : UIContainerView
    {
        public CustomTitleViewContainer(View view) : base(view)
        {
            TranslatesAutoresizingMaskIntoConstraints = false;
        }

        public override CGSize IntrinsicContentSize => UILayoutFittingExpandedSize;
    }
}