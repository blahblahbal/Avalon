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
    float frameY = (coords.y * uImageSize0.y - uSourceRect.y) / uSourceRect.w;
    float2 cloudCoords = (coords * float2(5, 1) * uImageSize0 - uSourceRect.xy) / uImageSize1;
    float4 cloud = tex2D(uImage1, cloudCoords + float2(uTime * 0.01, uTime * 0.2)) * 2;
    
    float brightness = dot(color.rgb, float3(0.5, 0.5, 0.5)) * (1.8 + (sin(uTime * 10) * 0.2));
    brightness *= (cloud.r + 1) * frameY;
    color.rgb = lerp(float3(0, 0.1, 0), float3(0.7, 1, 0), brightness);
    
    color.rgb *= color.a;
    color -= tex2D(uImage0, coords + float2(cloud.r / uImageSize0.x * 2, cloud.r / uImageSize0.y * 6)) * 0.3 * frameY;
    float4 color2 = tex2D(uImage0, coords + float2(0, 2 / uImageSize0.y)) * 0.2;
    
    if (!any(color))
        return color;
    return color * sampleColor.a;
}
    
technique Technique1
{
    pass CursedFlameDye
    {
        PixelShader = compile ps_2_0 ArmorBasic();
    }
}