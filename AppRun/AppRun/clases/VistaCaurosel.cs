using AppRun.clases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;


namespace AppRun
{
    class VistaCaurosel: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


        private ObservableCollection<Image> Images;

        public ObservableCollection<Image> images
        {
            get { return Images; }
            set{Images = value;PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("images")); }
        }


        public VistaCaurosel()
        {
            images = new ObservableCollection<Image>();
            addData();
        }

        private void addData()
        {
            images.Add(new Image{ imgSource = "https://images.pexels.com/photos/1954524/pexels-photo-1954524.jpeg" });
            images.Add(new Image { imgSource = "https://images.pexels.com/photos/3931124/pexels-photo-3931124.jpeg" });
            images.Add(new Image { imgSource = "https://images.pexels.com/photos/4491805/pexels-photo-4491805.jpeg"});
            images.Add(new Image  {  imgSource = " https://images.pexels.com/photos/5067733/pexels-photo-5067733.jpeg"});
        }
    }
}
