Working with images and NativeAOT
=================================

This is simple example how anyone can work with images in NativeAOT.

This application uses https://github.com/kant2002/winformscominterop to provide COM interop to this NativeAOT application.

Supplementary code for article: https://codevision.medium.com/how-do-you-use-system-drawing-in-nativeaot-bde0389daacb

# What's changed

As of Preview 7 there no need to have custom ComWrappers for `System.Drawings.Common` so having just

```
<PackageReference Include="System.Drawing.Common" Version="6.0.0-preview.7.*" />
```

reduce need for external libraries.