using Html5QrcodeBlazor.Wrapper.Util;
using Microsoft.JSInterop;

namespace Html5QrcodeBlazor.Wrapper
{
    public class Html5QrcodeReader : JSModule
    {
        public bool IsCameraInitialized { get; set; } = false;
        private DotNetObjectReference<Html5QrcodeReader>? dotNetHelper;
        /// <summary>
        /// Event raised on successfull barcode scan
        /// </summary>
        public event Action<string>? OnBarcodeScan;
        public Html5QrcodeReader(IJSRuntime js)
            : base(js, "./_content/Html5QrcodeBlazor.Wrapper/barcodeScanner.js")
        {

        }
        public async Task Start(string divId = "reader", string facingMode = "environment", int fps = 10)
        {
            if (IsCameraInitialized)
                return;
            dotNetHelper ??= DotNetObjectReference.Create(this);
            await InvokeVoidAsync("setDotNetHelper", dotNetHelper);
            await InvokeVoidAsync("initializeHtml5Qrcode", divId, facingMode, fps);
            IsCameraInitialized = true;
        }
        /// <summary>
        /// Easy Mode - With end to end scanner user interface
        /// </summary>
        public async Task Render(string divId = "reader", int fps = 10)
        {
            if (IsCameraInitialized)
                return;
            dotNetHelper ??= DotNetObjectReference.Create(this);
            await InvokeVoidAsync("setDotNetHelper", dotNetHelper);
            await InvokeVoidAsync("renderHtml5Qrcode", divId, fps);
            IsCameraInitialized = true;
        }
        public async Task Stop()
        {
            if (!IsCameraInitialized)
                return;

            try
            {
                await InvokeVoidAsync("stopCamera");
            }
            catch (Exception)
            {
                //skip
            }
            IsCameraInitialized = false;
        }
        [JSInvokable("HandleCameraBarcodeScan")]
        public void HandleCameraBarcodeScan(string barcode)
        {
            OnBarcodeScan?.Invoke(barcode);
        }

    }
}