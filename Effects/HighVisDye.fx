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

    float gray = dot(color.rgb, float3(0.2, 0.75, 0.15));
    float3 Glowcolor = float4(float3(gray, gray, gray), 1);
    float brightness = dot(color.rgb, float3(0.5, 0.5, 0.5));

    color.rgb = lerp(color.rgb, Glowcolor.rgb * uColor * brightness * 3, brightness);
    return color * sampleColor;
}
    
technique Technique1
{
    pass HighVisDye
    {
        PixelShader = compile ps_2_0 ArmorBasic();
    }
}