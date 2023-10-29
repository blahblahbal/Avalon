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
    float2 cloudCoords2 = ((coords * uImageSize0 - uSourceRect.xy) / float2(uImageSize1.x * 0.8, uImageSize1.y)) + 0.7;
    float2 cloudCoords = ((coords * uImageSize0 - uSourceRect.xy) / float2(uImageSize1.x, uImageSize1.y) * 0.5) + 0.5;
    //float cloud = tex2D(uImage1, cloudCoords + float2((sin(uTime * 0.05) * 0.5) + 0.5, sin(uTime * 0.005) * 1)) + tex2D(uImage1, cloudCoords2 + float2((sin(uTime * 0.05) * 0.5) + 0.5, sin(uTime * 0.005) * 1));
    
    float cloud = tex2D(uImage1, cloudCoords + float2(uTime * 0.04, uTime * 0.01)) + tex2D(uImage1, cloudCoords2 + float2(uTime * 0.03, uTime * 0.01));
    //float cloud2 = tex2D(uImage1, cloudCoords + float2(uTime * 0.01, uTime * 0.02)) + tex2D(uImage1, cloudCoords2 + float2(uTime * 0.02, uTime * 0.03) + cloud);
    
    if ((cloud <= 0.93 && cloud >= 0.88) || (cloud <= 0.73 && cloud >= 0.68) || (cloud <= 0.53 && cloud >= 0.48) || (cloud <= 0.33 && cloud >= 0.28) || (cloud <= 0.13 && cloud >= 0.08))
    {
        color.rgb *= 1.3;
    }
    if ((cloud <= 0.95 && cloud >= 0.83) || (cloud <= 0.75 && cloud >= 0.63) || (cloud <= 0.55 && cloud >= 0.43))
    {
        color.rgb *= 1.3;
    }
    
    color.rgb -= float3(0.4, 0.1, 0);
    
    //color.rgb = lerp(color.rgb, float3(0.5,0.9,1), 0.2);
    //color.rgb = clamp(color.rgb, 0, 1);
    return color * sampleColor;
}
    
technique Technique1
{
    pass AquaDye
    {
        PixelShader = compile ps_2_0 ArmorBasic();
    }
}