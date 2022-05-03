using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TweenComponentSystem
{
    public class DOFade : DOBase
    {
        public enum FadeSource
        {
            Image,
            CanvasGroup,
            SpriteRenderer,
            Material
        }

        public float startValue;
        public float endValue = 1;

        public FadeSource fadeSource;
        public int materialIndex;

        Image sourceImage;
        SpriteRenderer sourceSprite;
        CanvasGroup sourceCanvasGroup;
        Material sourceMaterial;

        internal override void VirtualEnable()
        {
            switch (fadeSource)
            {
                case FadeSource.Image:
                    sourceImage = GetComponent<Image>();
                    break;
                case FadeSource.CanvasGroup:
                    sourceCanvasGroup = GetComponent<CanvasGroup>();
                    break;
                case FadeSource.SpriteRenderer:
                    sourceSprite = GetComponent<SpriteRenderer>();
                    break;
                case FadeSource.Material:
                    sourceMaterial = GetComponent<Renderer>().materials[materialIndex];
                    break;
            }
        }
        public override void DO()
        {
            switch (fadeSource)
            {
                case FadeSource.Image:
                    if (Application.isPlaying)
                    {
                        if (!allowTwin)
                        {
                            if (DOTween.IsTweening(sourceImage))
                                sourceImage.DOKill(true);
                        }
                        tween = sourceImage.DOFade(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).OnComplete(() => doComplete.Invoke());
                        base.DO();
                    }
                    else
                    {
                        sourceImage = GetComponent<Image>();
#if UNITY_EDITOR
                        Undo.RecordObject(sourceImage, "Changed Image Fade");
#endif
                        Color color = sourceImage.color;
                        color.a = endValue;
                        sourceImage.color = color;
#if UNITY_EDITOR
                        EditorUtility.SetDirty(sourceImage);
#endif
                    }
                    break;
                case FadeSource.CanvasGroup:
                    if (Application.isPlaying)
                    {
                        if (!allowTwin)
                        {
                            if (DOTween.IsTweening(sourceCanvasGroup))
                                sourceCanvasGroup.DOKill(true);
                        }
                        tween = sourceCanvasGroup.DOFade(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).OnComplete(() => doComplete.Invoke());
                        base.DO();
                    }
                    else
                    {
                        sourceCanvasGroup = GetComponent<CanvasGroup>();
#if UNITY_EDITOR
                        Undo.RecordObject(sourceCanvasGroup, "Changed CanvasGroup Fade");
#endif
                        sourceCanvasGroup.alpha = endValue;
#if UNITY_EDITOR
                        EditorUtility.SetDirty(sourceCanvasGroup);
#endif
                    }
                    break;
                case FadeSource.SpriteRenderer:
                    if (Application.isPlaying)
                    {
                        if (!allowTwin)
                        {
                            if (DOTween.IsTweening(sourceSprite))
                                sourceSprite.DOKill(true);
                        }
                        tween = sourceSprite.DOFade(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).OnComplete(() => doComplete.Invoke());
                        base.DO();
                    }
                    else
                    {
                        sourceSprite = GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
                        Undo.RecordObject(sourceSprite, "Changed SpriteRenderer Fade");
#endif
                        Color color2 = sourceSprite.color;
                        color2.a = endValue;
                        sourceSprite.color = color2;
#if UNITY_EDITOR
                        EditorUtility.SetDirty(sourceSprite);
#endif
                    }
                    break;
                case FadeSource.Material:
                    if (Application.isPlaying)
                    {
                        if (!allowTwin)
                        {
                            if (DOTween.IsTweening(sourceMaterial))
                                sourceMaterial.DOKill(true);
                        }
                        tween = sourceMaterial.DOFade(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).OnComplete(() => doComplete.Invoke());
                        base.DO();
                    }
                    else
                    {
                        sourceMaterial = GetComponent<Renderer>().sharedMaterials[materialIndex];
#if UNITY_EDITOR
                        Undo.RecordObject(sourceMaterial, "Changed Material Fade");
#endif
                        Color color3 = sourceMaterial.color;
                        color3.a = endValue;
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
            switch (fadeSource)
            {
                case FadeSource.Image:
                    if (Application.isPlaying)
                    {
                        if (!allowTwin)
                        {
                            if (DOTween.IsTweening(sourceImage))
                                sourceImage.DOKill(true);
                        }
                        tween = sourceImage.DOFade(startValue, doDuration).SetDelay(revertDelay).SetEase(twoEase ? revertEase : doEase).OnComplete(() => doRevertComplete.Invoke());
                        base.DORevert();
                    }
                    else
                    {
                        sourceImage = GetComponent<Image>();
#if UNITY_EDITOR
                        Undo.RecordObject(sourceImage, "Changed Image Fade");
#endif
                        Color color = sourceImage.color;
                        color.a = startValue;
                        sourceImage.color = color;
#if UNITY_EDITOR
                        EditorUtility.SetDirty(sourceImage);
#endif
                    }
                    break;
                case FadeSource.CanvasGroup:
                    if (Application.isPlaying)
                    {
                        if (!allowTwin)
                        {
                            if (DOTween.IsTweening(sourceCanvasGroup))
                                sourceCanvasGroup.DOKill(true);
                        }
                        tween = sourceCanvasGroup.DOFade(startValue, doDuration).SetDelay(revertDelay).SetEase(twoEase ? revertEase : doEase).OnComplete(() => doRevertComplete.Invoke());
                        base.DORevert();
                    }
                    else
                    {
                        sourceCanvasGroup = GetComponent<CanvasGroup>();
#if UNITY_EDITOR
                        Undo.RecordObject(sourceCanvasGroup, "Changed CanvasGroup Fade");
#endif
                        sourceCanvasGroup.alpha = startValue;
#if UNITY_EDITOR
                        EditorUtility.SetDirty(sourceCanvasGroup);
#endif
                    }
                    break;
                case FadeSource.SpriteRenderer:
                    if (Application.isPlaying)
                    {
                        if (!allowTwin)
                        {
                            if (DOTween.IsTweening(sourceSprite))
                                sourceSprite.DOKill(true);
                        }
                        tween = sourceSprite.DOFade(startValue, doDuration).SetDelay(revertDelay).SetEase(twoEase ? revertEase : doEase).OnComplete(() => doRevertComplete.Invoke());
                        base.DORevert();
                    }
                    else
                    {
                        sourceSprite = GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
                        Undo.RecordObject(sourceSprite, "Changed SpriteRenderer Fade");
#endif
                        Color color2 = sourceSprite.color;
                        color2.a = startValue;
                        sourceSprite.color = color2;
#if UNITY_EDITOR
                        EditorUtility.SetDirty(sourceSprite);
#endif
                    }
                    break;
                case FadeSource.Material:
                    if (Application.isPlaying)
                    {
                        if (!allowTwin)
                        {
                            if (DOTween.IsTweening(sourceMaterial))
                                sourceMaterial.DOKill(true);
                        }
                        tween = sourceMaterial.DOFade(startValue, doDuration).SetDelay(revertDelay).SetEase(twoEase ? revertEase : doEase).OnComplete(() => doRevertComplete.Invoke());
                        base.DORevert();
                    }
                    else
                    {
                        sourceMaterial = GetComponent<Renderer>().sharedMaterials[materialIndex];
#if UNITY_EDITOR
                        Undo.RecordObject(sourceMaterial, "Changed Material Fade");
#endif
                        Color color3 = sourceMaterial.color;
                        color3.a = endValue;
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
            switch (fadeSource)
            {
                case FadeSource.Image:
                    if (Application.isPlaying)
                        sourceImage.DOKill(true);
                    else
                        sourceImage = GetComponent<Image>();
#if UNITY_EDITOR
                    Undo.RecordObject(sourceImage, "Changed Image Fade");
#endif
                    Color color = sourceImage.color;
                    color.a = startValue;
                    sourceImage.color = color;
#if UNITY_EDITOR
                    EditorUtility.SetDirty(sourceImage);
#endif
                    break;
                case FadeSource.CanvasGroup:
                    if (Application.isPlaying)
                        sourceCanvasGroup.DOKill(true);
                    else
                        sourceCanvasGroup = GetComponent<CanvasGroup>();
#if UNITY_EDITOR
                    Undo.RecordObject(sourceCanvasGroup, "Changed CanvasGroup Fade");
#endif
                    sourceCanvasGroup.alpha = startValue;
#if UNITY_EDITOR
                    EditorUtility.SetDirty(sourceCanvasGroup);
#endif
                    break;
                case FadeSource.SpriteRenderer:
                    if (Application.isPlaying)
                        sourceSprite.DOKill(true);
                    else
                        sourceSprite = GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
                    Undo.RecordObject(sourceSprite, "Changed SpriteRenderer Fade");
#endif
                    Color color2 = sourceSprite.color;
                    color2.a = startValue;
                    sourceSprite.color = color2;
#if UNITY_EDITOR
                    EditorUtility.SetDirty(sourceSprite);
#endif
                    break;
                case FadeSource.Material:
                    if (Application.isPlaying)
                        sourceMaterial.DOKill(true);
                    else
                        sourceMaterial = GetComponent<Renderer>().sharedMaterials[materialIndex];
#if UNITY_EDITOR
                    Undo.RecordObject(sourceMaterial, "Changed Material Fade");
#endif
                    Color color3 = sourceMaterial.color;
                    color3.a = startValue;
                    sourceMaterial.color = color3;
#if UNITY_EDITOR
                    EditorUtility.SetDirty(sourceMaterial);
#endif
                    break;
            }
        }
        public override void DOLoop()
        {
            switch (fadeSource)
            {
                case FadeSource.Image:
                    if (DOTween.IsTweening(sourceImage))
                        sourceImage.DOKill(true);
                    tween = sourceImage.DOFade(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).SetLoops(-1, loopType);
                    break;
                case FadeSource.CanvasGroup:
                    if (DOTween.IsTweening(sourceCanvasGroup))
                        sourceCanvasGroup.DOKill(true);
                    tween = sourceCanvasGroup.DOFade(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).SetLoops(-1, loopType);
                    break;
                case FadeSource.SpriteRenderer:
                    if (DOTween.IsTweening(sourceSprite))
                        sourceSprite.DOKill(true);
                    tween = sourceSprite.DOFade(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).SetLoops(-1, loopType);
                    break;
                case FadeSource.Material:
                    if (DOTween.IsTweening(sourceMaterial))
                        sourceMaterial.DOKill(true);
                    tween = sourceMaterial.DOFade(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).SetLoops(-1, loopType);
                    break;
            }
        }
        public override void Kill()
        {
            switch (fadeSource)
            {
                case FadeSource.Image:
                    if (DOTween.IsTweening(sourceImage))
                        sourceImage.DOKill(true);
                    break;
                case FadeSource.CanvasGroup:
                    if (DOTween.IsTweening(sourceCanvasGroup))
                        sourceCanvasGroup.DOKill(true);
                    break;
                case FadeSource.SpriteRenderer:
                    if (DOTween.IsTweening(sourceSprite))
                        sourceSprite.DOKill(true);
                    break;
                case FadeSource.Material:
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
