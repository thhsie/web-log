import { Modal, Button, Group, Text, Title } from "@mantine/core";

interface ConfirmationModalProps {
  opened: boolean;
  onClose: () => void;
  onConfirm: () => void;
  title: string;
  text: string;
  confirmButtonColor?: string;
}

export const ConfirmationModal: React.FC<ConfirmationModalProps> = ({
  opened,
  onClose,
  onConfirm,
  title,
  text,
  confirmButtonColor = "blue",
}) => {
  return (
    <Modal
      opened={opened}
      onClose={onClose}
      title={
        <Title order={5} mb="sm" mt="sm">
          {title}
        </Title>
      }
      centered
      radius={"lg"}
    >
      <Text size="md" mb="xl" >
        {text}
      </Text>
      <Group mt="md" mb="sm">
        <Button
          variant="light"
          radius="xl"
          size="md"
          onClick={onClose}
          color="gray"
        >
          Cancel
        </Button>
        <Button color={confirmButtonColor} radius="xl" size="md" onClick={onConfirm}>
          Confirm
        </Button>
      </Group>
    </Modal>
  );
};
