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