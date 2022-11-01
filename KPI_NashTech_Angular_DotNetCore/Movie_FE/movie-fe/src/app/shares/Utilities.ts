export function toBase64(file: File) {
    return new Promise((resolve, rejects) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = (error) => rejects(error);
    });
}

export function showAPIErrors(response: any): string[] {
    const result: string[] = [];
    if (response.error) {
        if (typeof response.error === 'string') {
            result.push(response.error);
        }else if (Array.isArray(response.error)) {
            response.error.forEach((value:any) => result.push(value.description));

        } else {
            const mapErrors = response.error.errors;
            const entries = Object.entries(mapErrors);
            entries.forEach((arr: any[]) => {
                const field = arr[0];
                arr[1].forEach((errorMessage: any) => {
                    result.push(`${field}: ${errorMessage}`);
                })
            })
        }
    }
    return result;
}

export function formatDate(date: Date) {
    date = new Date(date);
    const format = new Intl.DateTimeFormat('en', {
        year: 'numeric',
        month: '2-digit',
        day: '2-digit'
    });
    const [
        { value: month }, ,
        { value: day }, ,
        { value: year }
    ] = format.formatToParts(date);

    // format to yyyy-MM-dd
    return `${year}-${month}-${day}`;
}