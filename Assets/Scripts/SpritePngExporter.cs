using System.IO;
using UnityEngine;

// 사용 방법 : Resources파일 안에 이미지 넣기 -> 이미지 파일의 이름을 "MyTextureSheet"로 변경 -> 이미지파일 클릭하여 Advanced의 Read/Write 체크 -> 실행

public class SpritePngExporter : MonoBehaviour
{
    // 주어진 텍스처에서 특정 Rect 영역을 추출하여 PNG로 저장
    public static void SaveTextureSliceAsPNG(Texture2D texture, Rect sliceRect, string filePath)
    {
        // 디렉토리 존재 여부 확인 및 생성
        string directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory); // 디렉토리 없으면 생성
        }

        // 텍스처가 읽기/쓰기 가능한지 확인하고, 불가능하면 클론 생성
        if (!texture.isReadable)
        {
            Texture2D readableTexture = new Texture2D(texture.width, texture.height, texture.format, false);
            readableTexture.SetPixels(texture.GetPixels());
            readableTexture.Apply();
            texture = readableTexture; // 이제 읽을 수 있는 텍스처로 대체
        }

        // 텍스처에서 주어진 Rect 영역의 픽셀을 추출
        Color[] pixels = texture.GetPixels((int)sliceRect.x, (int)sliceRect.y, (int)sliceRect.width, (int)sliceRect.height);

        // 새로운 Texture2D 생성 (슬라이스된 부분만큼 크기를 맞춤)
        Texture2D newTexture = new Texture2D((int)sliceRect.width, (int)sliceRect.height);

        // 추출한 픽셀 데이터를 새로운 텍스처에 설정
        newTexture.SetPixels(pixels);
        newTexture.Apply();

        // PNG로 변환하여 저장
        byte[] pngData = newTexture.EncodeToPNG();
        File.WriteAllBytes(filePath, pngData);

        // 로그로 파일 경로 출력
        Debug.Log("Saved PNG to: " + filePath);
    }


    // 예시: 스프라이트 시트에서 특정 슬라이스를 PNG로 저장하는 테스트 함수
    void Start()
    {
        // 예시: Resources 폴더에서 텍스처 로드 (스프라이트 시트)
        Texture2D texture = Resources.Load<Texture2D>("MyTextureSheet");

        if (texture != null)
        {
            // 스프라이트 시트에서 사용하는 스프라이트들을 가져옵니다.
            // Sprite[]는 Sprite Renderer나 Sprite의 슬라이스 정보를 가져오는 부분입니다.
            Sprite[] sprites = Resources.LoadAll<Sprite>("MyTextureSheet");

            // 각 스프라이트의 rect를 기반으로 PNG로 저장
            for (int i = 0; i < sprites.Length; i++)
            {
                // 각 스프라이트의 rect 값을 가져옵니다.
                Rect spriteRect = sprites[i].rect;

                // 슬라이스된 이미지를 저장할 경로를 설정
                string filePath = "C:/Users/kms36/Desktop/BrokenDownZero/PngExporter/slicedTexture_" + i + ".png";
                SaveTextureSliceAsPNG(texture, spriteRect, filePath);
            }
        }
        else
        {
            Debug.LogError("텍스처를 찾을 수 없습니다.");
        }
    }
}