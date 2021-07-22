using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppRun.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged

    {

        private bool isBusy=false;
        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")

        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }


        protected void SetValue<T>(ref T backingFieled, T value, [CallerMemberName] string propertyName = null)

        {

            if (EqualityComparer<T>.Default.Equals(backingFieled, value))

            {

                return;

            }

            backingFieled = value;

            OnPropertyChanged(propertyName);

        }



        protected virtual void OnPropertyChangeds([CallerMemberName] string propertyName = null)

        {

            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)

            {

                handler(this, new PropertyChangedEventArgs(propertyName));

            }

        }
         protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName=null)
        {
            if (object.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        } 

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);

        }
    }
}
