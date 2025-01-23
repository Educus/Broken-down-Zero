using System.IO;
using UnityEngine;

// ��� ��� : Resources���� �ȿ� �̹��� �ֱ� -> �̹��� ������ �̸��� "MyTextureSheet"�� ���� -> �̹������� Ŭ���Ͽ� Advanced�� Read/Write üũ -> ����

public class SpritePngExporter : MonoBehaviour
{
    // �־��� �ؽ�ó���� Ư�� Rect ������ �����Ͽ� PNG�� ����
    public static void SaveTextureSliceAsPNG(Texture2D texture, Rect sliceRect, string filePath)
    {
        // ���丮 ���� ���� Ȯ�� �� ����
        string directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory); // ���丮 ������ ����
        }

        // �ؽ�ó�� �б�/���� �������� Ȯ���ϰ�, �Ұ����ϸ� Ŭ�� ����
        if (!texture.isReadable)
        {
            Texture2D readableTexture = new Texture2D(texture.width, texture.height, texture.format, false);
            readableTexture.SetPixels(texture.GetPixels());
            readableTexture.Apply();
            texture = readableTexture; // ���� ���� �� �ִ� �ؽ�ó�� ��ü
        }

        // �ؽ�ó���� �־��� Rect ������ �ȼ��� ����
        Color[] pixels = texture.GetPixels((int)sliceRect.x, (int)sliceRect.y, (int)sliceRect.width, (int)sliceRect.height);

        // ���ο� Texture2D ���� (�����̽��� �κи�ŭ ũ�⸦ ����)
        Texture2D newTexture = new Texture2D((int)sliceRect.width, (int)sliceRect.height);

        // ������ �ȼ� �����͸� ���ο� �ؽ�ó�� ����
        newTexture.SetPixels(pixels);
        newTexture.Apply();

        // PNG�� ��ȯ�Ͽ� ����
        byte[] pngData = newTexture.EncodeToPNG();
        File.WriteAllBytes(filePath, pngData);

        // �α׷� ���� ��� ���
        Debug.Log("Saved PNG to: " + filePath);
    }


    // ����: ��������Ʈ ��Ʈ���� Ư�� �����̽��� PNG�� �����ϴ� �׽�Ʈ �Լ�
    void Start()
    {
        // ����: Resources �������� �ؽ�ó �ε� (��������Ʈ ��Ʈ)
        Texture2D texture = Resources.Load<Texture2D>("MyTextureSheet");

        if (texture != null)
        {
            // ��������Ʈ ��Ʈ���� ����ϴ� ��������Ʈ���� �����ɴϴ�.
            // Sprite[]�� Sprite Renderer�� Sprite�� �����̽� ������ �������� �κ��Դϴ�.
            Sprite[] sprites = Resources.LoadAll<Sprite>("MyTextureSheet");

            // �� ��������Ʈ�� rect�� ������� PNG�� ����
            for (int i = 0; i < sprites.Length; i++)
            {
                // �� ��������Ʈ�� rect ���� �����ɴϴ�.
                Rect spriteRect = sprites[i].rect;

                // �����̽��� �̹����� ������ ��θ� ����
                string filePath = "C:/Users/kms36/Desktop/BrokenDownZero/PngExporter/slicedTexture_" + i + ".png";
                SaveTextureSliceAsPNG(texture, spriteRect, filePath);
            }
        }
        else
        {
            Debug.LogError("�ؽ�ó�� ã�� �� �����ϴ�.");
        }
    }
}