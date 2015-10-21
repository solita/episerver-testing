# Solita EPiServer testing

Contains classes for aiding with test automation of EPiServer websites. Most of these classes are fakes or mocks of EPiServer
types.

## What's included

* FakeContentRepository: simplified fake implementation of content repository.
* FakeContentArea: content area with no dependency to EPiServer context.
* FakeUrlResolver: simplified fake implementation of UrlResolver.
* CreatePage/CreateSharedBlock: factories for creating instances of pages and shared blocks. 
These handle all the necessary proxy magic.

## Requirements

.NET 4.6 (for the test project). Currently bound against EPiServer CMS 8.11, but should be compatible with newer versions as well.

## Usage example

```
[TestMethod]
public void TestArticleImport()
{            
    var contentRepository = new FakeContentRepository();
    var importer = new ArticleImporter(contentRepository, ...);

    importer.Import(fileStream);

    var articles = contentRepository.Contents.OfType<ArticlePage>();
    Assert.AreEqual(2, articles.Count(), "Number of articles imported");
}    
```

## Source code

Source code is in [a Git repository in Solita's Deveo](https://deus.solita.fi/Solita/projects/episerver/repositories/solita-testing-episerver/tree/master).

Master branch is automatically compiled into a nuget in Solita's internal nuget feed.