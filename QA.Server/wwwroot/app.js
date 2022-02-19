
// todo - let's only support one mark down editor per page... 
var simplemde
function setupMarkDownEdit(divTextArea)
{
    setTimeout(() => _setupMarkDownEdit(divTextArea), 1000);
}

function _setupMarkDownEdit(divTextArea){
    simplemde = new SimpleMDE({ element: document.getElementById(divTextArea) })
}

function getMarkDownEditContent(divTextArea){
    return simplemde.value();
}

function getMarkDownEditHtmlContent(divTextArea){
    console.log("mark down editor value");
    var html = simplemde.previewRender();
    console.log(html);
    return html;
}