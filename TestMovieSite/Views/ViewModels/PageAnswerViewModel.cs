namespace TestMovieSite.Views.ViewModels
{
    public class PageAnswerViewModel<T> : BaseViewModel
    {
        public T ViewModel { get; set; }
        public string Message { get; set; }
    }
}
