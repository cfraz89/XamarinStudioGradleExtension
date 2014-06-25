Xamarin Studio Gradle Extension
======================

This is an extension to Xamarin Studio (most likely also works with MonoDevelop) which integrates basic support for the gradle build system, via [xamarin-gradle-plugins](https://github.com/cfraz89/xamarin-gradle-plugins).

Currently it supports the following project options:

- Fetch Dependencies: Will use the gradle plugin to fetch dependant dlls for the project via maven/ivy.
- Publish Local: Publishes a project's output dlls to the local Maven repository.
- Publish: Publishes a project's output dlls to all Maven repositories defined in build.gradle.

Additionally, the extension replaces the standard build process to execute the gradle build task, which ensures dependency dlls have been downloaded before building. Of course the project's build.gradle can be extended to perform other tasks before and after build/publishing steps.