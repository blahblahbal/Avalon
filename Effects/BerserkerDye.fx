sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float2 uTargetPosition;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;
float4 uLegacyArmorSourceRect;
float2 uLegacyArmorSheetSize;
    
float4 ArmorBasic(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    
    if (!any(color))
        return color;

    const float blackThreshold = 0.3;
    const float threshold = 0.46;
    float gray = dot(color.rgb, float3(0.2, 0.75, 0.15));
    color = float4(float3(gray, gray, gray), 1);

    if (color.r > threshold || color.g > threshold || color.b > threshold)
    {
        color.rgb *= float3(0.29, 1, 0.984);
    }
    else if (color.r > blackThreshold || color.g > blackThreshold || color.b > blackThreshold)
    {
        color.rgb *= float3(0.957, 0.266, 0.474) * 1.7;
    }
    else
    {
        color.rgb *= float3(0.957, 0.266, 0.474) * 1.2;
    }

    return color * sampleColor;
}
    
technique Technique1
{
    pass BerserkerDye
    {
        PixelShader = compile ps_2_0 ArmorBasic();
    }
}