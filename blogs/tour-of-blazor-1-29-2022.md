In the next 3 years, I can see many enterprises considering Blazor server for their web application needs.  I have enjoyed my journey of building Angular 2+ clients for .NET core applications.  From an architecture perspective, I do feel like modern web application development has become more complex in the past 5 years.  In this post, I wanted to share my raw impressions of test driving Blazor server application development.  Please keep in mind that modern Angular has become my favorite front-end JavaScript SPA tech. In my brief test drive, I enjoy the potential to drive simplicity back into modern web applications.  If you're trying to hire a full-stack Angular/DotNetCore application, your search becomes more challenging.  Since a Blazor dev focuses on 80% C# for code-behind, your enterprise hiring complexities can be simplified.  From a quick analysis of Blazor server, Microsoft has matched most of the major benefits of React's component model.  As an experienced .NET guy, I love the simple component model syntax as compared to React.  While Blazor does not have the experience and depth of open source community as React or Angular, I'm impressed with the COTS and open source trends happening around Blazor.  I believe that Blazor designers have tried to match the best ideas from Angular/React while trying to keep things simple.  In general, I feel like .NET core has helped make C# development fun again.  I feel like Blazor has just added some gas to that fire.

## Why I like Angular for SPA development?

- *Writing clean and testable code:* In Agile environments, we have to embrace change of requirements and business conditions.  On my teams, we strive to make sure that key business logic services in client and server code has good unit tests or integration tests.  Given Miško Hevery served as one of the founders of Angular, Google has engineered Angular for many levels of test automation. 
- *Component model:* I really love the component model of Angular.  Since I really enjoy C#, I have also liked exploring TypeScript for component code. As you get started with Angular, you can accomplish a lot of UX complexity using inputs and outputs to components.  The component system enables you to break down complex UX into smaller "Lego" blocks.
- *Scaling JavaScript:* Since Angular embraces TypeScript, it does not really block you from JavaScript, the powerful ecosystem of JavaScript libraries, and browser API's .   For an enterprise team, we enjoy the benefits of type safety for refactoring, code completion and tooling support.  This will continue to be strong benefit for Angular.
- *Angular has an amazing community.*  To be honest, Angular has a higher concept count as compared with React.  I, however, feel that the Angular team does a good job promoting great documentation.  While great complexity is possible in Angular, the community actively and critically talks about ways to keep Angular sustainable, clean, and maintainable.  If you want new toys for Angular, you'll find plenty on GitHub.

## Tear down of a Blazor component

In order to learn anything, I believe you got to build something.  I started exploring Blazor by building a small question and answer portal(i.e. like StackOverFlow ) for my team at work. In general, users will be able to post questions using Markdown syntax.  Users will have the ability to post answers to questions and up vote the best information.  In the following code tear down, we'll explore the add question component of the application.

If you'd like to see the whole application in context, you can check out my repo here:
[https://github.com/michaelprosario/q-and-a](https://github.com/michaelprosario/q-and-a)

``` csharp
@page "/add-question"
@inherits AddQuestionComponentBase

<h1>Ask a question</h1>
<br>
<h3>Title</h3>
<HxInputText @bind-Value="@Record.Name" Placeholder="Enter question title" />
<br>
<h3>Question Details</h3>

<MarkdownEditor @bind-Value="@Record.Content" ValueHTMLChanged="@OnMarkdownValueHTMLChanged" />

<h3>Tags</h3>
<HxInputText @bind-Value="@Record.Tags" Placeholder="Enter tags" />
<br>
@if (ValidationFailures.Count > 0)
{
    <ul>
        @foreach (var validationFailure in ValidationFailures)
        {
            <li>@validationFailure</li>
        }
    </ul>
}
<HxButton OnClick="OnSaveQuestion" Color="ThemeColor.Primary">Save Question</HxButton>
```

As you skim over the code, it feels very similar to the component code you'd author in Angular or React.  At the very top of the page, we have added routing to the "add-question" url.  We have also expressed the idea that the component code or Razor code has a C# class to encapsulate behavior.

``` csharp
@page "/add-question"
@inherits AddQuestionComponentBase
```

In this brief code snippet, you can see that I've integrated 2 Blazor UI libraries
- [https://github.com/havit/Havit.Blazor](https://github.com/havit/Havit.Blazor): This project includes a Bootstrap 5 component bundle for common UX elements, grids, and patterns. 

- [PSC.Blazor.Components.MarkdownEditor](https://github.com/erossini/BlazorMarkdownEditor): This component provides a simple mark down editor. In the code sample below, you can see that we can bind the value to our record content.  As the user changes data in the editor, those changes are written into the data bound variable.  (Input concept).   Further, the editor throws events when data changes in the editor.  In my application, I am able to capture the HTML representation of post using this event.

``` csharp
<MarkdownEditor 
@bind-Value="@Record.Content" 
ValueHTMLChanged="@OnMarkdownValueHTMLChanged" 
/>
```

## CSharp code behind 

``` csharp
using DocumentStore.Core.Requests;
using DocumentStore.Core.Services.DocumentStore.Core.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using QA.Core.Entities;
using QA.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QA.Server
{
    public partial class AddQuestionComponentBase : ComponentBase
    {

        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] private IDocumentsService<Question> DocumentsService { get; set; }
        public Question Record{ get; set; }
        private string markdownHtml = "";

        public IList<ValidationFailure> ValidationFailures = new List<ValidationFailure>();
        
        private void OnNewRecord()
        {            
            Record = new Question
            {
                Id = Guid.NewGuid().ToString()
            };            
        }

        private async Task OnSave()
        {
            ValidationFailures = new List<ValidationFailure>();

            Record.CreatedBy = "system";
            Record.PermaLink = Record.Name;
            Record.Abstract = markdownHtml;
            Record.HtmlContent = markdownHtml;

            var questionValidator = new QuestionValidator();
            var validationResults = questionValidator.Validate(this.Record);
            if(validationResults.Errors.Count() > 0)
            {
                ValidationFailures = validationResults.Errors;
                return;
            }

            var command = new StoreDocumentCommand<Question>
            {
                Document = Record,
                UserId = "system"
            };
            var response = await this.DocumentsService.StoreDocument(command);
            if (!response.Ok())
            {
                ValidationFailures = response.ValidationErrors;
            }
            else
            {
                NavigationManager.NavigateTo($"view-question/{Record.Id}", true);
            }
        }

        protected Task OnMarkdownValueHTMLChanged(string value)
        {
            markdownHtml = value;
            return Task.CompletedTask;
        }
        
        protected override async Task OnInitializedAsync()
        {
            OnNewRecord();
        }

        protected async Task OnSaveQuestion()
        {
            await OnSave();
        }
    }
}

```
In the component base class, you have the opportunity to describe the behavior of the Blazor component and describe dependencies.  In Angular, we inject service dependencies into a component class through the constructor.  In Blazor, you execute the same concept through property injection and "inject" attributes.  Like Angular, properties of the component base class become available to the Razor markup file. (i.e. Record)  For some of my Unity 3D amgios, this feels a lot like a "GameObject" script.

``` csharp
namespace QA.Server
{
    public partial class AddQuestionComponentBase : ComponentBase
    {

        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] private IDocumentsService<Question> DocumentsService { get; set; }
        public Question Record{ get; set; }
        private string markdownHtml = "";

```

When we boot the component, we execute the new record setup method.
``` csharp
protected override async Task OnInitializedAsync()
{
    OnNewRecord();
}
```

When we save data from the form into the database, I love the simplicity.  In the Angular and DotNetCore server architecture, we have a strong separation of backend services from the front-end work.  On the DotNetCore side of the house, we will expose business logic services through a set of controller classes and related security guards.  On the Angular/TypeScript client, the client developer will need to create a proxy services to connect to each business logic service.  While this is not hard, it's a tedious chore for someone to do.  Some teams have used OpenAPI/Swagger to code generate their proxy classes.  It's great to delegate to robots!  Check out NSwagStudio if you're interested.
[https://github.com/RicoSuter/NSwag/wiki/NSwagStudio](https://github.com/RicoSuter/NSwag/wiki/NSwagStudio)

In the world of Blazor Server, all of this complexity melts away.  The client component model can the business services without the junk of JSON, proxies and controllers.  Since I'm using Steve Smith's Clean Architecture pattern ([https://github.com/ardalis/CleanArchitecture](https://github.com/ardalis/CleanArchitecture)), I was able to adapt my project setup quickly to abandon the former "controller" centered architecture.  If I get a postive response from the service, I can navigate the user to another page.

``` csharp
private async Task OnSave()
{
    // mapping and validation code goes here ...........
    var command = new StoreDocumentCommand<Question>
    {
        Document = Record,
        UserId = "system"
    };
    var response = await this.DocumentsService.StoreDocument(command);
    if (!response.Ok())
    {
        ValidationFailures = response.ValidationErrors;
    }
    else
    {
        NavigationManager.NavigateTo($"view-question/{Record.Id}", true);
    }
}
```

In a future blog post, I'll try to outline a few other benefits to Blazor.  For now, I wanted to close with a final consideration if you're thinking about leaving Angular for Blazor.   In the Blazor community, the technology shifts toward favoring C# over calling raw JavaScript.   In the .NET community, there's a significant number of enterprise developers who love this story.   If you're building a form over data application, Blazor will do just fine.  Most of the major component vendors have built C# API layers to integrate with their components. 

Check out this post to review the open-source and COTS solutions connected to Blazor.

[https://github.com/AdrienTorris/awesome-blazor](https://github.com/AdrienTorris/awesome-blazor?utm_source=pocket_mylist)

If your web application client needs to interface with a JavaScript library or an obscure browser feature, you need to consider the cost of building proxy classes(JavaScript interop classes) to connect your C# client code to the browser.

My initial test drives of Blazor and their open source community feel productive so far.   I look forward to seeing this technology grow.

