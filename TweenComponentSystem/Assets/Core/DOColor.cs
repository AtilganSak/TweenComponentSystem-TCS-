using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TweenComponentSystem
{
    public class DOColor : DOBase
    {
        public enum ColorSource
        {
            Image,
            SpriteRenderer,
            Material
        }

        public Color startColor = Color.white;
        public Color endColor = Color.white;

        public ColorSource colorSource;
        public int materialIndex;

        Image sourceImage;
        SpriteRenderer sourceSprite;
        CanvasGroup sourceCanvasGroup;
        Material sourceMaterial;

        internal override void VirtualEnable()
        {
            switch (colorSource)
            {
                case ColorSource.Image:
                    sourceImage = GetComponent<Image>();
                    break;
                case ColorSource.SpriteRenderer:
                    sourceSprite = GetComponent<SpriteRenderer>();
                    break;
                case ColorSource.Material:
                    sourceMaterial = GetComponent<Renderer>().materials[materialIndex];
                    break;
            }
        }
        public override void DO()
        {
            switch (colorSource)
            {
                case ColorSource.Image:
                    if (Application.isPlaying)
                    {
                        if (!allowTwin)
                        {
                            if (DOTween.IsTweening(sourceImage))
                                sourceImage.DOKill(true);
                        }
                        tween = sourceImage.DOColor(endColor, doDuration).SetDelay(doDelay).SetEase(doEase).OnComplete(() => doComplete.Invoke());
                        base.DO();
                    }
                    else
                    {
                        sourceImage = GetComponent<Image>();
#if UNITY_EDITOR
                        Undo.RecordObject(sourceImage, "Changed Image Fade");
#endif
                        Color color = sourceImage.color;
                        color = endColor;
                        sourceImage.color = color;
#if UNITY_EDITOR
                        EditorUtility.SetDirty(sourceImage);
#endif
                    }
                    break;
                case ColorSource.SpriteRenderer:
                    if (Application.isPlaying)
                    {
                        if (!allowTwin)
                        {
                            if (DOTween.IsTweening(sourceSprite))
                                sourceSprite.DOKill(true);
                        }
                        tween = sourceSprite.DOColor(endColor, doDuration).SetDelay(doDelay).SetEase(doEase).OnComplete(() => doComplete.Invoke());
                        base.DO();
                    }
                    else
                    {
                        sourceSprite = GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
                        Undo.RecordObject(sourceSprite, "Changed SpriteRenderer Fade");
#endif
                        Color color2 = sourceSprite.color;
                        color2 = endColor;
                        sourceSprite.color = color2;
#if UNITY_EDITOR
                        EditorUtility.SetDirty(sourceSprite);
#endif
                    }
                    break;
                case ColorSource.Material:
                    if (Application.isPlaying)
                    {
                        if (!allowTwin)
                        {
                            if (DOTween.IsTweening(sourceMaterial))
                                sourceMaterial.DOKill(true);
                        }
                        tween = sourceMaterial.DOColor(endColor, doDuration).SetDelay(doDelay).SetEase(doEase).OnComplete(() => doComplete.Invoke());
                        base.DO();
                    }
                    else
                    {
                        sourceMaterial = GetComponent<Renderer>().sharedMaterials[materialIndex];
#if UNITY_EDITOR
                        Undo.RecordObject(sourceMaterial, "Changed Material Fade");
#endif
                        Color color3 = sourceMaterial.color;
                        color3 = endColor;
                        sourceMaterial.color = color3;
#if UNITY_EDITOR
                        EditorUtility.SetDirty(sourceMaterial);
#endif
                    }
                    break;
            }
        }
        public override void DORevert()
        {
            switch (colorSource)
            {
                case ColorSource.Image:
                    if (Application.isPlaying)
                    {
                        if (!allowTwin)
                        {
                            if (DOTween.IsTweening(sourceImage))
                                sourceImage.DOKill(true);
                        }
                        tween = sourceImage.DOColor(startColor, twoDuration ? revertDuration : doDuration).SetDelay(twoDelay? revertDelay : doDelay).
                            SetEase(twoEase ? revertEase : doEase).OnComplete(() => doRevertComplete.Invoke());
                        base.DORevert();
                    }
                    else
                    {
                        sourceImage = GetComponent<Image>();
#if UNITY_EDITOR
                        Undo.RecordObject(sourceImage, "Changed Image Fade");
#endif
                        Color color = sourceImage.color;
                        color = startColor;
                        sourceImage.color = color;
#if UNITY_EDITOR
                        EditorUtility.SetDirty(sourceImage);
#endif
                    }
                    break;
                case ColorSource.SpriteRenderer:
                    if (Application.isPlaying)
                    {
                        if (!allowTwin)
                        {
                            if (DOTween.IsTweening(sourceSprite))
                                sourceSprite.DOKill(true);
                        }
                        tween = sourceSprite.DOColor(startColor, twoDuration ? revertDuration : doDuration).SetDelay(twoDelay ? revertDelay : doDelay).
                            SetEase(twoEase ? revertEase : doEase).OnComplete(() => doRevertComplete.Invoke());
                        base.DORevert();
                    }
                    else
                    {
                        sourceSprite = GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
                        Undo.RecordObject(sourceSprite, "Changed SpriteRenderer Fade");
#endif
                        Color color2 = sourceSprite.color;
                        color2 = startColor;
                        sourceSprite.color = color2;
#if UNITY_EDITOR
                        EditorUtility.SetDirty(sourceSprite);
#endif
                    }
                    break;
                case ColorSource.Material:
                    if (Application.isPlaying)
                    {
                        if (!allowTwin)
                        {
                            if (DOTween.IsTweening(sourceMaterial))
                                sourceMaterial.DOKill(true);
                        }
                        tween = sourceMaterial.DOColor(startColor, twoDuration ? revertDuration : doDuration).SetDelay(twoDelay ? revertDelay : doDelay).
                            SetEase(twoEase ? revertEase : doEase).OnComplete(() => doRevertComplete.Invoke());
                        base.DORevert();
                    }
                    else
                    {
                        sourceMaterial = GetComponent<Renderer>().sharedMaterials[materialIndex];
#if UNITY_EDITOR
                        Undo.RecordObject(sourceMaterial, "Changed Material Fade");
#endif
                        Color color3 = sourceMaterial.color;
                        color3 = startColor;
                        sourceMaterial.color = color3;
#if UNITY_EDITOR
                        EditorUtility.SetDirty(sourceMaterial);
#endif
                    }
                    break;
            }
        }
        public override void ResetDO()
        {
            switch (colorSource)
            {
                case ColorSource.Image:
                    if (Application.isPlaying)
                        sourceImage.DOKill(true);
                    else
                        sourceImage = GetComponent<Image>();
#if UNITY_EDITOR
                    Undo.RecordObject(sourceImage, "Changed Image Fade");
#endif
                    Color color = sourceImage.color;
                    color = startColor;
                    sourceImage.color = color;
#if UNITY_EDITOR
                    EditorUtility.SetDirty(sourceImage);
#endif
                    break;
                case ColorSource.SpriteRenderer:
                    if (Application.isPlaying)
                        sourceSprite.DOKill(true);
                    else
                        sourceSprite = GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
                    Undo.RecordObject(sourceSprite, "Changed SpriteRenderer Fade");
#endif
                    Color color2 = sourceSprite.color;
                    color2 = startColor;
                    sourceSprite.color = color2;
#if UNITY_EDITOR
                    EditorUtility.SetDirty(sourceSprite);
#endif
                    break;
                case ColorSource.Material:
                    if (Application.isPlaying)
                        sourceMaterial.DOKill(true);
                    else
                        sourceMaterial = GetComponent<Renderer>().sharedMaterials[materialIndex];
#if UNITY_EDITOR
                    Undo.RecordObject(sourceMaterial, "Changed Material Fade");
#endif
                    Color color3 = sourceMaterial.color;
                    color3 = startColor;
                    sourceMaterial.color = color3;
#if UNITY_EDITOR
                    EditorUtility.SetDirty(sourceMaterial);
#endif
                    break;
            }
        }
        public override void DOLoop()
        {
            switch (colorSource)
            {
                case ColorSource.Image:
                    if (DOTween.IsTweening(sourceImage))
                        sourceImage.DOKill(true);
                    tween = sourceImage.DOColor(endColor, doDuration).SetDelay(doDelay).SetEase(doEase).SetLoops(-1, loopType);
                    break;
                case ColorSource.SpriteRenderer:
                    if (DOTween.IsTweening(sourceSprite))
                        sourceSprite.DOKill(true);
                    tween = sourceSprite.DOColor(endColor, doDuration).SetDelay(doDelay).SetEase(doEase).SetLoops(-1, loopType);
                    break;
                case ColorSource.Material:
                    if (DOTween.IsTweening(sourceMaterial))
                        sourceMaterial.DOKill(true);
                    tween = sourceMaterial.DOColor(endColor, doDuration).SetDelay(doDelay).SetEase(doEase).SetLoops(-1, loopType);
                    break;
            }
        }
        public override void Kill()
        {
            switch (colorSource)
            {
                case ColorSource.Image:
                    if (DOTween.IsTweening(sourceImage))
                        sourceImage.DOKill(true);
                    break;
                case ColorSource.SpriteRenderer:
                    if (DOTween.IsTweening(sourceSprite))
                        sourceSprite.DOKill(true);
                    break;
                case ColorSource.Material:
                    if (Application.isPlaying)
                    {
                        if (DOTween.IsTweening(sourceMaterial))
                            sourceMaterial.DOKill(true);
                    }
                    break;
            }
        }
    }
}
