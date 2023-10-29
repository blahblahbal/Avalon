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
    
struct vertexShaderStruct
{
    float3 pos      : POSITION0;
    float2 texCoord : TEXCOORD0;
};

struct pixelShaderStruct
{
    float4 position : POSITION0;
    float2 texCoord : TEXCOORD0;
};

float maxX = 32;
float maxY = 32;

float4 ArmorBasic(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    
    if (!any(color))
        return color;

    float4 color2 = 0;
    float2 position = coords;

    for (int x = 0; x < 4; x++)
    {
        for (int y = 0; y < 4; y++)
        {
            color2 += tex2D(uImage0, position + float2(x, y));
            
        }
    }

    color2 /= 2;
    
    const float blackThreshold = 0.3;
    const float t1 = 0.33;
    const float higherT = 0.4;
    float gray = dot(color2.rgb, float3(0.4, 0.4, 0.4));
    color.rgb = float3(gray, gray, gray);
    
    if (gray.r > 0.4)
    {
        color.rgb *= float3(0.29, 1, 0.984) * 1.4; // cyan ish
    }
    else if (gray.r > 0.32)
    {
        color.rgb *= float3(0.737, 0.541, 0.735) * 1.6;
    }
    else if (gray.r > 0.2)
    {
        color.rgb *= float3(0.957, 0.266, 0.474) * 2.2;
    }
    else
    {
        color.rgb *= float3(0.957, 0.266, 0.474) * 2.2;
    }

    return color * sampleColor * tex2D(uImage0, coords).a;
}

technique Technique1
{
    pass BerserkerDye
    {
        PixelShader = compile ps_2_0 ArmorBasic();
    }
}