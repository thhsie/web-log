import React from "react";
import useScrollbarSize from "react-scrollbar-size";

export const Scrollbar: React.FC = () => {
  const { width } = useScrollbarSize();
  if (width > 0) {
    const gap = getComputedStyle(document.documentElement).getPropertyValue(
      "--row-padding-right-base"
    );
    document.documentElement.style.setProperty(
      "--row-padding-right-scrollable",
      `${parseInt(gap) - width}px`
    );
    document.documentElement.style.setProperty(
      "--scrollbar-width",
      `${width}px`
    );
  }
  return <></>;
};
