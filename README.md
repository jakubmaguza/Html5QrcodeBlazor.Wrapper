# Html5QrcodeBlazor.DemoWasm

This is blazor wrapper over [html5-qrcode](https://github.com/mebjas/html5-qrcode) JS libarary.
I was trying using other libararies allready ported to Blazor, but **html5-qrcode** is reading barcodes way better.

[html5-qrcode documentation](https://scanapp.org/html5-qrcode-docs/docs/intro)

# Usage
1. Register service:
`builder.Services.AddScoped<Html5QrcodeReader>();`
2. Add script tag in index.html: `<script src="/_content/Html5QrcodeBlazor.Wrapper/html5-qrcode.min.js"></script>`
3. Inject service in Razor page: `@inject Html5QrcodeReader cameraBarcodeReader`
4. Subscribe to `OnBarcodeScan` event:

```c#
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (firstRender)
    {
        cameraBarcodeReader.OnBarcodeScan += HandleBarcodeScanDebounced;
    }
}
public async Task HandleBarcodeScan(string barcode)
{
    foundBarcode = barcode.Trim();
}
```

At this point you have 2 options:
- Easy Mode which includes UI
- Pro Mode with more customizations


For Pro Mode check [Index.razor](https://github.com/jakubmaguza/Html5QrcodeBlazor.Wrapper/blob/master/Html5QrcodeBlazor.DemoWasm/Pages/Index.razor).

For Easy Mode check [EasyMode.razor](https://github.com/jakubmaguza/Html5QrcodeBlazor.Wrapper/blob/master/Html5QrcodeBlazor.DemoWasm/Pages/EasyMode.razor).

Remember to implement `@implements IAsyncDisposable`:
```c#
public async ValueTask DisposeAsync()
{
    cameraBarcodeReader.OnBarcodeScan -= HandleBarcodeScanDebounced;
    await cameraBarcodeReader.Stop();
}
```

Explore **Html5QrcodeBlazor.DemoWasm** project for more details.
