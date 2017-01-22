﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoScheduler.Base;
using TodoScheduler.Converters;
using Xamarin.Forms;

namespace TodoScheduler.Controls
{
    public class BasePage : ContentPage
    {
        public BasePage()
        {
            SetBinding(NavigationProperty, new Binding(nameof(Navigation), converter: new NavigationConverter()));

            if (Device.OS == TargetPlatform.iOS)
                Padding = new Thickness(0, 20, 0, 0);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = (ViewModelBase)this.BindingContext;
            if (viewModel != null)
                viewModel.Appearing();
        }
    }
}
