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
    
    color.rgb /= 9;
    for (int i = -1; i < 2; i++)
    {
        for (int j = -1; j < 2; j++)
        {
            color.rgb += tex2D(uImage0, coords + (float2(i * 2,j * 2) / uImageSize0)) / 9;
        }
	}
	color.rgb *= color.a; // this fixes issues with bilinear filtered pixels on the outlines of sprites
    return color * sampleColor;
}
    
technique Technique1
{
    pass BlurDye
    {
        PixelShader = compile ps_2_0 ArmorBasic();
    }
}