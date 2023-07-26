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
    if (any(color))
    color.rgb = lerp(color.rgb, float3(1, 1, 1), 0.4);
    
    float2 cloudCoords = (coords * uImageSize0 - uSourceRect.xy) / float2(uImageSize1.x * 3,uImageSize1.y * 0.9);
    float cloud = tex2D(uImage1, cloudCoords + float2(sin(uTime * 0.01) * 5, sin(uTime * 0.005) * 4));
    
    float2 cloudCoords2 = (coords * uImageSize0 - uSourceRect.xy * 0.3) / float2(uImageSize1.x, uImageSize1.y);
    float cloud2 = tex2D(uImage1, cloudCoords2 + float2(sin(uTime * 0.005) * 4, sin(uTime * 0.0025) * 2));
    float4 color2 = tex2D(uImage0, coords + float2(((cloud * 10) - 5) / uImageSize0.x, ((cloud * 5) - 2.5) / uImageSize0.y));
    color.rgb += color2 * cloud2 * 0.8;
        
        
    if (!any(color))
        return color;
    
    //color.rgb = lerp(color.rgb, float3(0.5,0.5,0.5), 0.4);
    
    return color * sampleColor;
}
    
technique Technique1
{
    pass CloudyDye
    {
        PixelShader = compile ps_2_0 ArmorBasic();
    }
}