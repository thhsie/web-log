import { useState } from "react";
import {
  SegmentedControl,
  useMantineColorScheme,
  useComputedColorScheme,
  MantineColorScheme,
  ActionIcon,
} from "@mantine/core";
import { IconChevronRight, IconChevronLeft } from "@tabler/icons-react";
import cx from "clsx";
import classes from "./Sidebar.module.css";

export const Sidebar: React.FC = () => {
  const [isExpanded, setIsExpanded] = useState(false);
  const { colorScheme, setColorScheme } = useMantineColorScheme();
  useComputedColorScheme("light", { getInitialValueInEffect: true });

  return (
    <nav
      className={cx(classes.sidebar, {
        [classes.expanded]: isExpanded,
        [classes.reduced]: !isExpanded,
      })}
    >
      <div className={classes.sidebarMain}>
        <div
          className={cx(classes.segmentedControl, {
            [classes.hidden]: !isExpanded,
          })}
        >
          <SegmentedControl
            value={colorScheme}
            onChange={(value: string) =>
              setColorScheme(value as MantineColorScheme)
            }
            transitionTimingFunction="ease"
            fullWidth
            data={[
              { label: "Light", value: "light" },
              { label: "Dark", value: "dark" },
            ]}
          />
        </div>
        <div className={classes.toggleButton}>
          <ActionIcon
            variant="filled"
            color={isExpanded ? "blue" : "gray"}
            onClick={() => setIsExpanded(!isExpanded)}
          >
            {isExpanded ? (
              <IconChevronLeft size={24} />
            ) : (
              <IconChevronRight size={24} />
            )}
          </ActionIcon>
        </div>
      </div>
    </nav>
  );
};
