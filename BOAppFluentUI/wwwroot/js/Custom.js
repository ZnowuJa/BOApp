window.getPastedText = () => {
    return new Promise(resolve => {
        const clipboardData = window.clipboardData || window.DataTransfer;
        const pastedText = clipboardData.getData('Text');
        resolve(pastedText);
    });
};
window.downloadFile = function (filename, content) {
    var element = document.createElement('a');
    element.setAttribute('href', 'data:text/csv;charset=utf-8,' + encodeURIComponent(content));
    element.setAttribute('download', filename);

    element.style.display = 'none';
    document.body.appendChild(element);

    element.click();

    document.body.removeChild(element);
};

function goBack() {
    window.history.back();
};

window.getHistoryLength = function () {
    return window.history.length;
}
function openInNewTab(url) {
    window.open(url, '_blank');
};
getThemeMode = () => {
    const theme = localStorage.getItem('theme');
    if (theme) {
        const themeObject = JSON.parse(theme);
        return themeObject;
    }
    return 'light';
};

window.downloadFilePL = function (filename, base64Content) {
    //console.log("Downloading file:", filename);
    //console.log("Base64 content:", base64Content);

    const link = document.createElement('a');
    link.href = 'data:text/csv;charset=utf-8;base64,' + base64Content;
    link.download = filename;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}