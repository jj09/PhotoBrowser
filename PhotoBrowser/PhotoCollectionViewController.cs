using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Linq;

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

            var imageView = cell.ViewWithTag(100) as UIImageView;

            imageView.Image = _images.ElementAt(indexPath.Row);
            //imageView.AccessibilityLabel = "Description of image";

            cell.IsAccessibilityElement = true;
            cell.AccessibilityLabel = "this is Saqib";

            return cell;
        }

        public override CoreGraphics.CGSize GetSizeForChildContentContainer(IUIContentContainer contentContainer, CoreGraphics.CGSize parentContainerSize)
        {
            nfloat size = View.Frame.Width / 4 - 60;
            return new CoreGraphics.CGSize(size, size);
        }
    }
}