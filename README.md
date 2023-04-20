# FFT-Flipbook-Ocean-URP
An example of using the flipbook approach to achieve FFT wave motion in Unity.
Credits to [@GhislainGir](https://twitter.com/GhislainGir)'s tutorial --> [FFT Ocean Flipbook: How to create & sample one using Blender & UE](https://www.youtube.com/watch?v=rV6TJ7YDJY8&t=432s).

[WebGL Demo](https://erichu33.github.io/Flipbook-water)

***

https://user-images.githubusercontent.com/13420668/233394664-21e81623-b34f-43fc-81ba-a8d6176cfb3e.mp4





Basic setups of the shader.

1. Use Blender's ocean modifier to create a multi-wave ocean.
2. Change the shading of the ocean mesh inside Blender to normal color and height color.
<img src="https://user-images.githubusercontent.com/13420668/233391081-83108fa1-c613-47a3-9277-3931bffdd931.png" width="400">

3. Use "Rendering animation" to output the captured ocean surface result from Blender's camera. This gives you a sequence of images which can be later combined as a single sprite sheet.
<img src="https://user-images.githubusercontent.com/13420668/233390361-3fd99a35-c565-4406-a03c-907debee6f53.png" width="400">
    
4. Inside Unity, use the flipbook node to sample the normal and height sprite sheet. Adjust the normal and displacement accordingly.
<img src="https://user-images.githubusercontent.com/13420668/233390340-5dd60c43-3596-49f4-afbd-7f58c6c6d640.png" width="400">

### Distanced-LOD
The URP Shader Graph does not support tessellation. In this demo, I use instancing and manually choose LOD based on the distance from the camera. Please note that each LOD mesh's edge needs to have the same vertex count as the aligned mesh. Otherwise, a visible seam will occur.

https://user-images.githubusercontent.com/13420668/233386263-30b871a6-629d-46b5-a437-469ffa23f038.mp4