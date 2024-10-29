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
    
    float2 cloudCoords = (coords * uImageSize0 - uSourceRect.xy) / float2(uImageSize1.x * 2,uImageSize1.y * 3);
    float cloud = (tex2D(uImage1, cloudCoords + float2(uTime * 0.01, uTime * 0.005)) * 4) -2;
    float4 color2 = tex2D(uImage0, coords + float2((cloud * 4 - 6) / uImageSize0.x, (cloud - 1) / uImageSize0.y));
    float4 color = tex2D(uImage0, coords + float2((cloud * 0.5) / uImageSize0.x, (cloud * 0.5) / uImageSize0.y));
    color.rgb += color + color2 * cloud;
    color.rgb = clamp(color.rgb - 0.3, float3(0, 0, 0), float3(1.5, 1.5, 1.5));
    
    color.a = tex2D(uImage0, coords).a;
    
    if(any(color))
        color.rgb = lerp(float3(0.1, 0, 0.3), float3(0.4, 0.2, 0.7), dot(color.rgb, float3(0.5,0.5,0.5)));
    if (!any(color))
        return color;
    
    //color.rgb = lerp(color.rgb, float3(0.5,0.5,0.5), 0.4);
    
    return color * sampleColor;
}
    
technique Technique1
{
    pass PathogenDye
    {
        PixelShader = compile ps_2_0 ArmorBasic();
    }
}