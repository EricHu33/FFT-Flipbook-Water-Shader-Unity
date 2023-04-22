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

void HenyeyGreensteinPhase_float(float lightDotView, float g, out float Out)
{
    Out = 0;
    float denom = 1.0 + g * g + 2.0 * g * lightDotView;
    Out = (1.0 - g * g) / (denom * sqrt(denom));
}

void SchlickPhase_float(float dotNH, float refractionIndex, out float Out)
{
    Out = 0;
    float r0 = pow((1.0f - refractionIndex) / (1.0f + refractionIndex), 2.0f);
    float c = 1.0f - abs(dotNH);
    Out = r0 + (1.0f - r0) * pow(c, 5.0f);
}