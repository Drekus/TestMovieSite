﻿namespace TestMovieSite.Views.ViewModels
{
    public class BaseViewModel
    {
        public string Message { get; set; }
        public bool IsSuccessMessage { get; set; }
        public bool ShowMessage => !string.IsNullOrWhiteSpace(Message);
    }
}
