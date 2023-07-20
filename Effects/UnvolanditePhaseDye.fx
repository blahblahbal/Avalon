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

float4 ArmorBasic(float4 sampleColor : COLOR0, float2 coords : TEXCOORD) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
	
	if (!any(color))
		return color;
    float2 gradientCoords = (coords * uImageSize0 - uSourceRect.xy) / uLegacyArmorSheetSize;
    
    float gray = dot(color.rgb, float3(0.2, 0.75, 0.15));
    float rainbowBright = 0.7;
    
    if (gray > rainbowBright)
    {   
        float gradientPosition = gradientCoords.x + uTime;

        float R = 0.5 + 0.5 * cos(6.28 * (gradientPosition));
        float G = 0.5 + 0.5 * cos(6.28 * (gradientPosition));
        float B = 0.5 + 0.5 * cos(6.28 * (gradientPosition));
        R %= 3;
        G %= 3;
        B %= 3;
        R = clamp(R, 0, 1);
        G = clamp(G, 0, 1);
        B = clamp(B, 0, 1);
        return float4(R, G * 1.3, B * 1.05, 1) * sampleColor;
    }
    else
    {
        color.rgb = float3(gray * 1.6, gray * 1.2, gray);
        color.rgb *= sin(uTime * 2 + gradientCoords.x * uDirection * 16) * 0.2 + 0.9;
        return color * sampleColor;
    }
}
    
technique Technique1
{
    pass UnvolanditePhaseDye
    {
        PixelShader = compile ps_2_0 ArmorBasic();
    }
}