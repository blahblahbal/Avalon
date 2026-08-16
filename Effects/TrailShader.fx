matrix uTransformMatrix;
sampler2D uGradient : register(s0);
sampler2D uShape : register(s1);
sampler2D uErosion : register(s2);
float uTime;
float uScrollSpeed;
float uFatness;
float uOpacity;

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
    float3 Coord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
    float3 Coord : TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(in VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput) 0;
    output.Position = mul(input.Position, uTransformMatrix);
    output.Color = input.Color;
    output.Coord = input.Coord;
    return output;
}

float4 PixelShaderFunction(in VertexShaderOutput input) : COLOR0
{
    float2 uv = float2((input.Coord.x + (uTime * uScrollSpeed)) * sign(uScrollSpeed) % 1, (input.Coord.y - 0.5) / input.Coord.z + 0.5);
    uv.y = lerp(uv.y, 0.5, uFatness);
    return tex2D(uGradient, uv) * input.Color * tex2D(uShape, uv).r * uOpacity * tex2D(uErosion, uv).r;
}

technique Technique1
{
    pass Trail
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
        VertexShader = compile vs_3_0 VertexShaderFunction();
    }
}