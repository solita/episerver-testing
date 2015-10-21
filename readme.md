# Solita EPiServer testing

Contains classes for aiding with test automation of EPiServer websites. Most of these classes are fakes or mocks of EPiServer
types.

## What's included

* FakeContentRepository: simplified fake implementation of content repository.
* FakeContentArea: content area with no dependency to EPiServer context.
* FakeUrlResolver: simplified fake implementation of UrlResolver.
* CreatePage/CreateSharedBlock: factories for creating instances of pages and shared blocks. 
These handle all the necessary proxy magic.

## Source code

Source code is in [a Git repository in Solita's Deveo](https://deus.solita.fi/Solita/projects/episerver/repositories/solita-testing-episerver/tree/master).

Master branch is automatically compiled into a nuget in Solita's internal nuget feed.