using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MaterialDesignThemes.Wpf;

namespace SkyFlipR.Services.ErrorHandling;

public interface IErrorHandler
{
    public void HandleError(Exception exception);
}

public class ErrorHandler : IErrorHandler
{
    private readonly ISnackbarMessageQueue _snackbarMessageQueue;
    private readonly IDialogService _dialogService;

    public ErrorHandler(ISnackbarMessageQueue snackbarMessageQueue, IDialogService dialogService)
    {
        _snackbarMessageQueue = snackbarMessageQueue;
        _dialogService = dialogService;
    }

    public void HandleError(Exception exception)
    {
        _snackbarMessageQueue.Enqueue($"Error: {exception.Message}", "Details", () =>
        {
            var errorModel = new Services.ErrorHandling.ErrorModel("Error", exception.ToString());
            _dialogService.Show(new Services.ErrorHandling.ErrorHandlerView(errorModel));
        });
    }
}