using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using CoreAnimation;

namespace PhotoBrowser
{
    public partial class PhotoCollectionViewController : UICollectionViewController
    {
        private List<UIImage> _images;

        public PhotoCollectionViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            CollectionView.CollectionViewLayout = new PhotoLayout(View.Frame.Width / 4 - 10);

            _images = new List<UIImage>
            {
                UIImage.FromFile("seeing-ai-app.jpg"),
                UIImage.FromFile("seeing-ai-app2.jpg"),
                UIImage.FromFile("seeing-ai-app.jpg"),
                UIImage.FromFile("seeing-ai-app2.jpg"),
                UIImage.FromFile("seeing-ai-app.jpg"),
                UIImage.FromFile("seeing-ai-app2.jpg"),
                UIImage.FromFile("seeing-ai-app.jpg"),
                UIImage.FromFile("seeing-ai-app2.jpg")
            };
        }

        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return _images.Count;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell("photoViewCell", indexPath) as UICollectionViewCell;

            var image = _images.ElementAt(indexPath.Row);

            var imageButton = cell.ViewWithTag(100) as UIButton;
            imageButton.SetBackgroundImage(image, UIControlState.Normal);

            imageButton.TouchUpInside += (sender, e) => 
            {
                var previewViewController = new UIViewController();

                // TODO: adjust width/heigh based on image
                var imageView = new UIImageView(new CGRect(0, 0, View.Frame.Width, View.Frame.Height))
                {
                    Image = image
                };

                previewViewController.View.AddSubview(imageView);

                var closeButton = new UIButton(new CGRect(View.Frame.Width - 40, 20, 20, 20));
                closeButton.SetTitle("X", UIControlState.Normal);
                closeButton.AccessibilityLabel = "close";
                closeButton.TouchUpInside += (s, evt) => {
                    previewViewController.DismissViewController(true, null);
                };

                previewViewController.View.AddSubview(closeButton);

                this.ShowViewController(previewViewController, null);
            };

            cell.IsAccessibilityElement = true;
            cell.AccessibilityLabel = "image description";

            return cell;
        }
    }

    // https://developer.xamarin.com/guides/ios/user_interface/controls/uicollectionview/
    // https://forums.xamarin.com/discussion/72296/collectionview-cell-width-is-not-changing-on-different-screen-sizes
    public class PhotoLayout : UICollectionViewFlowLayout
    {
        public PhotoLayout(nfloat width)
        {
            this.ItemSize = new CGSize(width, width);
        }
    }
}