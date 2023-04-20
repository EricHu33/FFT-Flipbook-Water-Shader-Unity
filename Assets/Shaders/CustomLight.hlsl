void MainLight_float(out float3 Direction, out float3 Color, out float DistanceAtten)
{
    #ifdef SHADERGRAPH_PREVIEW
        Direction = normalize(float3(1, 1, -0.4));
        Color = float4(1, 1, 1, 1);
        DistanceAtten = 1;
    #else
        Light mainLight = GetMainLight();
        Direction = mainLight.direction;
        Color = mainLight.color;
        DistanceAtten = mainLight.distanceAttenuation;
    #endif
}