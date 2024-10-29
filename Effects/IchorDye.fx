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
    {
    
        float2 cloudCoords = (coords * uImageSize0 - uSourceRect.xy) / float2(uImageSize1.x * 0.1, uImageSize1.y);
        float cloud = tex2D(uImage1, cloudCoords - float2(0, uTime * 0.1));
    
        color.rgb += (color * cloud * 2) - 0.7;
        
        color.rgb += tex2D(uImage0, coords + float2(0, 2 / uImageSize0.y));
        
        float bright = dot(color.rgb * 2, float3(0.6, 0.5, -0.2));
        
        color.rgb = lerp(tex2D(uImage0, coords + float2(cloud / uImageSize0.x, cloud / uImageSize0.y)).rgb * sampleColor.rgb, lerp(float3(0.6, 0.3, 0), float3(1, 0.67, 0.2), clamp(bright, -1, 2)), cloud + 0.3);
        
		color.rgb *= color.a; // this fixes issues with bilinear filtered pixels on the outlines of sprites
        return color * sampleColor;
    }
    else
    {
        return tex2D(uImage0, coords);
    }
}
    
technique Technique1
{
    pass IchorDye
    {
        PixelShader = compile ps_2_0 ArmorBasic();
    }
}