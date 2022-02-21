
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

function setMarkDownEditContent(strContent){
    
    setTimeout(() => simplemde.value(strContent), 2000);    
}

function navigateToAddQuestion()
{
    window.location = "/add-question";
}

function navigateToSearchQuestions()
{
    window.location = "/search-questions";
}
