let dotNetHelper;
let html5QrCode;
let html5QrCodeScanner;

export async function initializeHtml5Qrcode(divName, facingMode, fps) {
    html5QrCode = new Html5Qrcode(divName);
    html5QrCode.start({ facingMode: facingMode }, {fps: fps}, handleCameraBarcodeScan);
}
export async function renderHtml5Qrcode(divName, fps, verbose) {
    html5QrCodeScanner = new Html5QrcodeScanner(divName, { fps: fps }, verbose);
    html5QrCodeScanner.render(handleCameraBarcodeScan);
}
export async function stopCamera() {
    if (html5QrCodeScanner)
        html5QrCodeScanner.clear();
    if (html5QrCode)
        await html5QrCode.stop();
}
export async function setDotNetHelper(value) {
    dotNetHelper = value;
}
async function handleCameraBarcodeScan(barcode, data) {
    await dotNetHelper.invokeMethodAsync('HandleCameraBarcodeScan', barcode);
}