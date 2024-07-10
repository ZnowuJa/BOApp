window.getPastedText = () => {
    return new Promise(resolve => {
        const clipboardData = window.clipboardData || window.DataTransfer;
        const pastedText = clipboardData.getData('Text');
        resolve(pastedText);
    });
};