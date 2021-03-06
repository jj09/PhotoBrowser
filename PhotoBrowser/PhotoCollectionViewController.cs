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

        public PhotoCollectionViewController() : base(new PhotoLayout(UIScreen.MainScreen.Bounds.Width / 4 - 10))
        {
            CollectionView.RegisterClassForCell(typeof(UICollectionViewCell), "photoViewCell");
            CollectionView.BackgroundColor = UIColor.Black;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

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

            var imageButton = new UIButton(new CGRect(0,0,View.Window.Frame.Width / 4 - 10,View.Window.Frame.Width / 4 - 10));
            cell.AddSubview(imageButton);
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
            cell.AccessibilityLabel = $"image {indexPath.Row}";

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
            this.SectionInset = new UIEdgeInsets(0, 0, 0, 0);
        }
    }
}