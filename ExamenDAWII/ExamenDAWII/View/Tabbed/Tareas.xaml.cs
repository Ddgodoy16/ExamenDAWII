﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agenda.View.Tabbed
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Tareas : ContentPage
	{
		public Tareas ()
		{
			InitializeComponent ();
		}
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }
    }
}