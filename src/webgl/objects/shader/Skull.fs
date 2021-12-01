#version 300 es
precision highp float;

uniform float time;
uniform float renderOutline;
uniform sampler2D noiseTex;

in vec3 vPosition;
in vec2 vUv;
in vec3 vColor;

out vec4 fragColor;

#pragma glslify: convertHsvToRgb = require(glsl-util/convertHsvToRgb)

void main() {
  // Flat Shading
  vec3 light = normalize(vec3(-0.8, 1.0, 0.2));
  vec3 normal = normalize(cross(dFdx(vPosition), dFdy(vPosition)));
  float diff = dot(normal, light);

  float noiseR = texture(
    noiseTex,
    normal.yz * 0.2 + vec2(time * 0.02, 0.0)
    ).r * 2.0 - 1.0;
  float noiseG = texture(
    noiseTex,
    normal.zx * 0.2 + vec2(0.0, time * 0.02)
    ).g * 2.0 - 1.0;
  float noiseB = texture(
    noiseTex,
    normal.xy * 0.2 - time * 0.02
    ).b * 2.0 - 1.0;
  float noise = length(vec3(noiseR, noiseG, noiseB));

  vec3 hsvNoise = vec3(noise * 0.1, noise * 0.1, -noise * 0.1);
  // vec3 hsv1 = vec3(0.99, 0.8, 0.4) + hsvNoise;
  // // vec3 hsv1 = vec3(0.0, 0.0, 0.0) + hsvNoise;
  // vec3 hsv2 = vec3(0.88, 0.0, 1.0) + hsvNoise;
  vec3 hsv1 = vec3(0.1, 0.1, 0.1) + hsvNoise;
  vec3 hsv2 = vec3(0.2, 0.2, 0.2) + hsvNoise;
  vec3 rgb = mix(convertHsvToRgb(hsv1), convertHsvToRgb(hsv2), diff);

  vec3 hsv3 = vec3(0.55, 0.05, 0.95);
  vec3 color = (rgb * (1.0 - vColor) + convertHsvToRgb(hsv3) * vColor) * (1.0 - renderOutline);
  vec3 colorOutline = vec3(1.0) * renderOutline;

  fragColor = vec4(color + colorOutline, 1.0);
}
