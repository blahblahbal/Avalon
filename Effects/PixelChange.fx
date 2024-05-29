sampler TextureSampler : register(s0);
float4 key_color;
float4 new_color;

float4 ChangePixel(float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{
	return tex2D(TextureSampler, texCoord) * 255;
}

technique PixelChange
{
	pass Pass0
	{
		PixelShader = compile ps_2_0 ChangePixel();
	}
}