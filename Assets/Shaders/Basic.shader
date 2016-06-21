Shader "Tutorial/Basic"
{
	Properties
	{
		_Color ("Main Color", Color) = (1, 0.5, 0.5, 1)
	}

	Subshader
	{
		Pass
		{
			Material { Diffuse [_Color] }

			Lighting On
		}
	}
}
