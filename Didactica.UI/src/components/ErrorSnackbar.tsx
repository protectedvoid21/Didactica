import { Alert, IconButton, Snackbar } from "@mui/material";
import { useState } from "react";
import CloseIcon from "@mui/icons-material/Close";

interface Props {
  error: string;
}

export const ErrorSnackbar = ({ error }: Props) => {
  const [open, setOpen] = useState(true);

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <Snackbar
      open={!!error && open}
      anchorOrigin={{ vertical: "top", horizontal: "center" }}
      autoHideDuration={10000}
      onClose={handleClose}
    >
      <Alert
        severity="error"
        className="items-center"
        variant='filled'>
        {error}
        <IconButton
          size="large"
          aria-label="close"
          onClick={handleClose}
        >
          <CloseIcon fontSize="medium" />
        </IconButton>
      </Alert>
    </Snackbar>
  );
};
